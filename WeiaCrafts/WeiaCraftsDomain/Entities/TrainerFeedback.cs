using WeiaCraftsDomain.Entities;

public class TrainerFeedback : Feedback
{
    public string TrainerUserName { get; set; } = string.Empty;
    public Trainer Trainer { get; set; } = null!;

}
