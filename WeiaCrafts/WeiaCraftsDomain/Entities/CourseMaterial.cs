namespace WeiaCraftsDomain.Entities;

public class CourseMaterial 
{

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int MaterialId { get; set; }
    public Material Material { get; set; } = null!;

}
