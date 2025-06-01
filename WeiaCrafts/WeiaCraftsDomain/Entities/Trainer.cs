namespace WeiaCraftsDomain.Entities;

public class Trainer : User
{
    public int TrainerId { get; set; }
    public double Rating { get; set; }
    public int RatingCount { get; set; }

    public bool IsAlsoTrainee { get; set; }

    public virtual List<Course> CreatedCourses { get; set; } = new List<Course>();

   
    public virtual ICollection<TrainerFeedback> FeedbacksReceived { get; set; } = new List<TrainerFeedback>(); 
    public virtual ICollection<TrainerRating> RatingsReceived { get; set; } = new List<TrainerRating>(); 
    public virtual ICollection<TrainerAlert> Alerts { get; set; } = new List<TrainerAlert>();
    public override string GetRole() => "Trainer";

}
