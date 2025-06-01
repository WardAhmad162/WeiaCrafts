using Microsoft.EntityFrameworkCore;
using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Interfaces;
using WeiaCraftsDomain.Entities;
using WeiaCraftsDomain.Enum;
using WeiaCraftsDomain.ValueObjects;
using WeiaCraftsInfrastructure.Data;

namespace WeiaCraftsApplication.Services;

public class CourseService : ICourseService
{
    private readonly WeiaCraftsContext _context;
    private readonly ICurrencyConverterService _currencyConverter;

    public CourseService(WeiaCraftsContext context, ICurrencyConverterService currencyConverter)
    {
        _context = context;
        _currencyConverter = currencyConverter;
    }

    public async Task<CourseDto?> GetCourseByIdAsync(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Trainer)
            .Include(c => c.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return null;

        return MapCourseToDto(course);
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _context.Courses
            .Include(c => c.Trainer)
            .Include(c => c.Lessons)
            .Where(c => !c.IsKidFriendly)
            .ToListAsync();

        return courses.Select(MapCourseToDto);
    }

    public async Task<IEnumerable<CourseDto>> GetCoursesByFilterAsync(CourseFilterOptionsDto filter)
    {
        var query = _context.Courses
            .Include(c => c.Trainer)
            .Include(c => c.Lessons)
            .Where(c => c.IsKidFriendly == filter.IsKidFriendly)
            .AsQueryable();

        if (filter.ParentCategory.HasValue)
            query = query.Where(c => c.WideRangeCategory == filter.ParentCategory);

        if (filter.Level.HasValue)
            query = query.Where(c => c.Level == filter.Level);

        if (filter.AccessType.HasValue)
            query = query.Where(c => c.AccessType == filter.AccessType);

        if (filter.Language.HasValue)
            query = query.Where(c => c.Language == filter.Language);

        if (filter.HasDocuments.HasValue)
        {
            if (filter.HasDocuments.Value)
                query = query.Where(c => c.CourseMaterials.Any());
            else
                query = query.Where(c => !c.CourseMaterials.Any());
        }

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.ToLower();
            query = query.Where(c => c.Title.ToLower().Contains(search) || c.Description.ToLower().Contains(search));
        }

        query = filter.SortBy switch
        {
            "newest" => query.OrderByDescending(c => c.DateOfCreation),
            "oldest" => query.OrderBy(c => c.DateOfCreation),
            "highestRated" => query.OrderByDescending(c => c.Rating),
            "lowestRated" => query.OrderBy(c => c.Rating),
            _ => query
        };

        var courses = await query.ToListAsync();

        return courses.Select(MapCourseToDto);
    }

    public async Task<IEnumerable<CourseDto>> GetCoursesByCategoryAsync(string categoryName)
    {
        if (Enum.TryParse<WideRangeCategory>(categoryName, true, out var categoryEnum))
        {
            var courses = await _context.Courses
                .Include(c => c.Trainer)
                .Include(c => c.Lessons)
                .Where(c => c.WideRangeCategory == categoryEnum && !c.IsKidFriendly)
                .ToListAsync();
            return courses.Select(MapCourseToDto);
        }
        throw new ArgumentException($"Invalid category name: {categoryName}");
    }

    public async Task<IEnumerable<CourseDto>> GetKidFriendlyCoursesAsync()
    {
        var courses = await _context.Courses
            .Include(c => c.Trainer)
            .Include(c => c.Lessons)
            .Where(c => c.IsKidFriendly)
            .ToListAsync();

        return courses.Select(MapCourseToDto);
    }

    public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto)
    {
        var course = new Course
        {
            Title = createCourseDto.Title,
            Description = createCourseDto.Description,
            Level = createCourseDto.Level,
            AccessType = createCourseDto.AccessType,
            Language = createCourseDto.Language,
            IsKidFriendly = createCourseDto.IsKidFriendly,
            Price = createCourseDto.Price,
            CoverImageURL = createCourseDto.CoverImageURL,
            PromoVideoURL = createCourseDto.PromoVideoURL,
            TrainerUserName = createCourseDto.TrainerUserName,
            WideRangeCategory = createCourseDto.WideRangeCategory,
            Status = CourseStatus.Archived,
            Rating = 0,
            RatingCount = 0,
            DateOfCreation = DateTime.UtcNow,
            LastUpdatedTimestamp = DateTime.UtcNow
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return MapCourseToDto(course);
    }

    public async Task<bool> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null) return false;

        if (updateCourseDto.Title != null) course.Title = updateCourseDto.Title;
        if (updateCourseDto.Description != null) course.Description = updateCourseDto.Description;
        if (updateCourseDto.Level.HasValue) course.Level = updateCourseDto.Level.Value;
        if (updateCourseDto.AccessType.HasValue) course.AccessType = updateCourseDto.AccessType.Value;
        if (updateCourseDto.Status.HasValue) course.Status = updateCourseDto.Status.Value;
        if (updateCourseDto.Language.HasValue) course.Language = updateCourseDto.Language.Value;
        if (updateCourseDto.IsKidFriendly.HasValue) course.IsKidFriendly = updateCourseDto.IsKidFriendly.Value;
        if (updateCourseDto.Price != null) course.Price = updateCourseDto.Price;
        if (updateCourseDto.CoverImageURL != null) course.CoverImageURL = updateCourseDto.CoverImageURL;
        if (updateCourseDto.PromoVideoURL != null) course.PromoVideoURL = updateCourseDto.PromoVideoURL;
        if (updateCourseDto.WideRangeCategory.HasValue) course.WideRangeCategory = updateCourseDto.WideRangeCategory.Value;

        course.LastUpdatedTimestamp = DateTime.UtcNow;

        _context.Entry(course).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<AmountWithCurrency> GetCoursePriceInUserCurrency(int courseId, string userCurrency)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course == null)
        {
            throw new KeyNotFoundException($"Course with ID {courseId} not found.");
        }
        return await _currencyConverter.ConvertAsync(course.Price, userCurrency);
    }

    private CourseDto MapCourseToDto(Course course)
    {
        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Level = course.Level,
            AccessType = course.AccessType,
            Status = course.Status,
            Language = course.Language,
            IsKidFriendly = course.IsKidFriendly,
            Price = course.Price,
            Rating = course.Rating,
            RatingCount = course.RatingCount,
            CoverImageURL = course.CoverImageURL,
            PromoVideoURL = course.PromoVideoURL,
            TrainerUserName = course.TrainerUserName,
            WideRangeCategory = course.WideRangeCategory,
            DateOfCreation = course.DateOfCreation,
            LastUpdatedTimestamp = course.LastUpdatedTimestamp,
            Lessons = course.Lessons?.Select(l => new LessonDto
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                SequenceOrder = l.SequenceOrder,
                ContentData = l.ContentData
            }).ToList() ?? new List<LessonDto>()

        };
    }
}

