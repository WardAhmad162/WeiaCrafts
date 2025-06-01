using WeiaCraftsDomain.Enum;

namespace WeiaCraftsApplication.DTOs;

public class MaterialDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MaterialType Type { get; set; }
    public string ContentUrl { get; set; } = string.Empty;
    public List<int> CourseIds { get; set; } = new();
} 