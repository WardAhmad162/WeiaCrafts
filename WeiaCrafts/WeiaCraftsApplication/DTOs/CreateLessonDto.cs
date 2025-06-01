using WeiaCraftsDomain.Enum;

namespace WeiaCraftsApplication.DTOs;

public class CreateLessonDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int SequenceOrder { get; set; }
    public LessonContentType ContentType { get; set; }
    public string? ContentData { get; set; }
    public int CourseId { get; set; }
}

