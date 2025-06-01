using WeiaCraftsDomain.Enum;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsApplication.DTOs;

public class UpdateCourseDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public CourseLevel? Level { get; set; }
    public CourseAccessType? AccessType { get; set; }
    public CourseStatus? Status { get; set; }
    public CourseLanguage? Language { get; set; }
    public bool? IsKidFriendly { get; set; }
    public AmountWithCurrency? Price { get; set; }
    public string? CoverImageURL { get; set; }
    public string? PromoVideoURL { get; set; }
    public WideRangeCategory? WideRangeCategory { get; set; }
}
