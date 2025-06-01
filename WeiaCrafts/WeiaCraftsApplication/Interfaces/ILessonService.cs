using WeiaCraftsApplication.DTOs;
using WeiaCraftsDomain.Entities;

namespace WeiaCraftsApplication.Interfaces;

public interface ILessonService
{
    Task<IEnumerable<Lesson>> GetLessonsAsync(int courseId);
    Task<Lesson?> GetLessonAsync(int id);
    Task<Lesson> CreateLessonAsync(int courseId, LessonDto dto);
    Task<bool> UpdateLessonAsync(int id, LessonDto dto);
    Task<bool> DeleteLessonAsync(int id);
}

