namespace WeiaCraftsApplication.DTOs;

public class AdminDto
{
    public int Id { get; set; }
    public string AdminName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Permissions { get; set; }
    public string? PlatformPolicies { get; set; }
    public string? ThirdPartyIntegrations { get; set; }
    public string? ChatbotUpdates { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
} 