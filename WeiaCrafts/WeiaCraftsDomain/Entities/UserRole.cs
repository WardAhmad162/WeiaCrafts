using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeiaCraftsDomain.Entities;

public class UserRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; } = string.Empty;

    [ForeignKey(nameof(UserName))]
    public required User User { get; set; }

    [Required]
    public int RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public required Role Role { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ExpiresAt { get; set; }

    public bool IsActive { get; set; } = true;

    [StringLength(256)]
    public string? AssignedBy { get; set; }
}
