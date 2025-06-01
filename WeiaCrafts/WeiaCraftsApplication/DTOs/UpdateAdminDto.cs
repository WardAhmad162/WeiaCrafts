using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsApplication.DTOs;

public class UpdateAdminDto
{
    [StringLength(50)]
    public string? AdminName { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100, MinimumLength = 6)]
    public string? Password { get; set; }

    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    [StringLength(1000)]
    public string? Permissions { get; set; }

    [StringLength(2000)]
    public string? PlatformPolicies { get; set; }

    [StringLength(1000)]
    public string? ThirdPartyIntegrations { get; set; }

    [StringLength(2000)]
    public string? ChatbotUpdates { get; set; }
} 