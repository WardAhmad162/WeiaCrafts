using WeiaCraftsDomain.Entities;

public class VideoFeedback : Feedback
{
    public int VideoId { get; set; }
    public Video Video { get; set; } = null!;
}
