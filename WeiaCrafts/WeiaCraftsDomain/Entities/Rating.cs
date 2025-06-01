namespace WeiaCraftsDomain.Entities;

public abstract class Rating
{
    public int Id { get; set; }

    public double Value { get; set; } 

    public string TraineeUserName { get; set; } = string.Empty;
    public Trainee Trainee { get; set; } = null!;

    public DateTime RatedAt { get; set; } = DateTime.UtcNow;
}
