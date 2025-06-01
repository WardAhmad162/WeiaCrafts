using WeiaCraftsDomain.Enum;

namespace WeiaCraftsDomain.Entities;

public class Material
{

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public MaterialType Type { get; set; }

    public string ContentUrl { get; set; } = string.Empty;

    public ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
}

