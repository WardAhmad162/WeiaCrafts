using Microsoft.EntityFrameworkCore;
using WeiaCraftsDomain.Entities;
using WeiaCraftsDomain.Enum;
using WeiaCraftsInfrastructure.Data;

namespace WeiaCraftsInfrastructure.Repositories;

public class CourseDataStore
{
    private readonly WeiaCraftsContext _context;

    public CourseDataStore(WeiaCraftsContext context)
    {
        _context = context;
    }

    public List<Course> GetAllCourses()
    {
        return _context.Courses
            .Include(c => c.WideRangeCategory)
            .Include(c => c.Trainer)
            .Include(c => c.CourseMaterials)
            .ToList();
    }

    public List<Course> GetBestRatedCourses()
    {
        return _context.Courses
            .OrderByDescending(c => c.Rating)
            .ToList();
    }

    public List<Course> GetLatestCourses()
    {
        return _context.Courses
            .OrderByDescending(c => c.DateOfCreation)
            .ToList();
    }

    public List<Course> GetCoursesWithDocuments()
    {
        return _context.Courses
            .Where(c => c.CourseMaterials.Any())
            .ToList();
    }

    public List<Course> GetCoursesByTrainer(string trainerUsername)
    {
        return _context.Courses
            .Where(c => c.TrainerUserName == trainerUsername)
            .ToList();
    }

    public List<Course> GetCoursesByLevel(CourseLevel level)
    {
        return _context.Courses
            .Where(c => c.Level == level)
            .ToList();
    }

    public List<dynamic> GetCoursesGroupedByCategory()
    {
        return _context.Courses
            .Include(c => c.WideRangeCategory)
            .GroupBy(c => c.WideRangeCategory)
            .Select(group => new
            {
                Category = group.Key,
                Courses = group.ToList()
            })
            .ToList<dynamic>();
    }
}
