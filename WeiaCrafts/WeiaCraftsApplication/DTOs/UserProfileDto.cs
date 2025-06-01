namespace WeiaCraftsApplication.DTOs;

public class UserProfileDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public List<string> Roles { get; set; } = new();
}
