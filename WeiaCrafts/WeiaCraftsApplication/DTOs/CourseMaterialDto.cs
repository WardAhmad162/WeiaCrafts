namespace WeiaCraftsApplication.DTOs;

public class CourseMaterialDto
{
    public int CourseId { get; set; }
    public int MaterialId { get; set; }
    public string MaterialName { get; set; } = string.Empty;
    public string ContentUrl { get; set; } = string.Empty;
} 