using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsDomain.Entities;

public class Admin 
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string AdminName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Permissions { get; set; }

    [StringLength(2000)]
    public string? PlatformPolicies { get; set; }

    [StringLength(1000)]
    public string? ThirdPartyIntegrations { get; set; }

    [StringLength(2000)]
    public string? ChatbotUpdates { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
