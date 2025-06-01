using WeiaCraftsDomain.Enum;

namespace WeiaCraftsApplication.DTOs;

public class EnrollmentDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public DateTime EnrollmentDate { get; set; }
    public EnrollmentStatus Status { get; set; }
    public double PercentageCompleted { get; set; }
    public int? LastAccessedLessonId { get; set; }
    public string? LastAccessedLessonTitle { get; set; }
    public DateTime? CompletionDate { get; set; }
} 