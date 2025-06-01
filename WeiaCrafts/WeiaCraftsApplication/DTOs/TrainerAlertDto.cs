namespace WeiaCraftsApplication.DTOs;

public class TrainerAlertDto
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string TrainerUserName { get; set; } = string.Empty;
} 