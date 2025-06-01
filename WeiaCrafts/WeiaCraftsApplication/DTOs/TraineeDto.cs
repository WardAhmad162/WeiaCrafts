namespace WeiaCraftsApplication.DTOs;

public class TraineeDto
{
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public List<CourseDto> EnrolledCourses { get; set; } = new();
    public List<CourseDto> CompletedCourses { get; set; } = new();
    public List<CourseDto> FavoriteCourses { get; set; } = new();
}

