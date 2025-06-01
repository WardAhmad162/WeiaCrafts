using WeiaCraftsDomain.Enum;

namespace WeiaCraftsApplication.DTOs;

public class LessonDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int SequenceOrder { get; set; }
    public LessonContentType ContentType { get; set; }
    public string? ContentData { get; set; } // URL, quiz, etc.
    public int CourseId { get; set; }
}

