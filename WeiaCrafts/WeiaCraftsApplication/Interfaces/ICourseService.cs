using WeiaCraftsApplication.DTOs;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsApplication.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<IEnumerable<CourseDto>> GetCoursesByCategoryAsync(string categoryName);
        Task<IEnumerable<CourseDto>> GetKidFriendlyCoursesAsync();
        Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task<bool> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto);
        Task<bool> DeleteCourseAsync(int id);
        Task<AmountWithCurrency> GetCoursePriceInUserCurrency(int courseId, string userCurrency);

        Task<IEnumerable<CourseDto>> GetCoursesByFilterAsync(CourseFilterOptionsDto filterOptions);
    }
}
