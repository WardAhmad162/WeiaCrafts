namespace WeiaCraftsDomain.Entities;

public class Video 
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public required Course Course { get; set; }

    public virtual List<VideoFeedback> FeedbacksRecieved { get; set; } = new List<VideoFeedback>();
    public virtual List<VideoRating> RatingsReceived { get; set; } = new List<VideoRating>();

}
