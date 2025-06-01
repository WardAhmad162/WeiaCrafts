using WeiaCraftsDomain.Entities;

public class VideoRating : Rating
{
    public int VideoId { get; set; }
    public Video Video { get; set; } = null!;
}
