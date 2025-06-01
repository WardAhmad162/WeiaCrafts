namespace WeiaCraftsApplication.DTOs;
public class TrainerDto
{
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public int RatingCount { get; set; }
    public List<CourseDto> CreatedCourses { get; set; } = new();
}
