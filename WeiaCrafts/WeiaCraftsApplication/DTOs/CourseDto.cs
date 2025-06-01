using WeiaCraftsDomain.Enum;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsApplication.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CourseLevel Level { get; set; }
    public CourseAccessType AccessType { get; set; }
    public CourseStatus Status { get; set; }
    public CourseLanguage Language { get; set; }
    public bool IsKidFriendly { get; set; }
    public AmountWithCurrency Price { get; set; }
    public double Rating { get; set; }
    public int RatingCount { get; set; }
    public string? CoverImageURL { get; set; }
    public string? PromoVideoURL { get; set; }
    public string? TrainerUserName { get; set; }
    public string? TrainerName { get; set; }
    public WideRangeCategory WideRangeCategory { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime LastUpdatedTimestamp { get; set; }
    public List<LessonDto> Lessons { get; set; } = new List<LessonDto>();
}