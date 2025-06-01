using WeiaCraftsDomain.Enum;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsApplication.DTOs;

public class CreateCourseDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CourseLevel Level { get; set; }
    public CourseAccessType AccessType { get; set; }
    public CourseLanguage Language { get; set; }
    public bool IsKidFriendly { get; set; }
    public AmountWithCurrency Price { get; set; }
    public string? CoverImageURL { get; set; }
    public string? PromoVideoURL { get; set; }
    public string TrainerUserName { get; set; }
    public WideRangeCategory WideRangeCategory { get; set; }

}
