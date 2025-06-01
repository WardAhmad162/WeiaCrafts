using WeiaCraftsDomain.Enum;

namespace WeiaCraftsApplication.DTOs;

public class CourseFilterOptionsDto
{
    public WideRangeCategory? ParentCategory { get; set; }
    public CourseLevel? Level { get; set; }
    public CourseAccessType? AccessType { get; set; }
    public CourseLanguage? Language { get; set; }
    public bool? HasDocuments { get; set; }
    public string? Search { get; set; }
    public string? SortBy { get; set; }
    public bool IsKidFriendly { get; set; } = false;
}