using WeiaCraftsDomain.Entities;

public class TrainerRating : Rating
{
    public string TrainerUserName { get; set; } = string.Empty;
    public Trainer Trainer { get; set; } = null!;
}