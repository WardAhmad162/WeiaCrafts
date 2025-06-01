namespace WeiaCraftsDomain.Entities;

public class Trainee : User
{
    public int TraineeId { get; set; }

    public virtual ICollection<Course> EnrolledCourses { get; set; } = new List<Course>();
    public virtual ICollection<Course> CompletedCourses { get; set; } = new List<Course>();
    public virtual ICollection<Course> FavoriteCourses { get; set; } = new List<Course>();

    public int RewardPoints { get; set; }

    public override string GetRole() => "Trainee";

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}


