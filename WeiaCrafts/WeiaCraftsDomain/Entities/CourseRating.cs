namespace WeiaCraftsDomain.Entities;

public class CourseRating : Rating
{
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
}
