namespace WeiaCraftsApplication.DTOs;

public class RatingDto
{
    public int Id { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string SubmittedBy { get; set; } = string.Empty;
}

