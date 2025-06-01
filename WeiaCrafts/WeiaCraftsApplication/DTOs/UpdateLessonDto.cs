using WeiaCraftsDomain.Enum;

namespace WeiaCraftsApplication.DTOs;

public class UpdateLessonDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? SequenceOrder { get; set; }
    public LessonContentType? ContentType { get; set; }
    public string? ContentData { get; set; }
}

