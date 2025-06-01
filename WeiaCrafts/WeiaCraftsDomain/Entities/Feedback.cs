namespace WeiaCraftsDomain.Entities;

public abstract class Feedback
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;
    public double Rating { get; set; }

    public string TraineeUserName { get; set; } = string.Empty;
    public Trainee Trainee { get; set; } = null!;

    public string? picturePath { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
