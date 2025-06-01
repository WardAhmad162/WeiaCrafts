using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsDomain.Entities;

public class Role
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string NormalizedName { get; set; } = string.Empty;

    [StringLength(256)]
    public string? Description { get; set; }

    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDefault { get; set; }

    public bool IsSystem { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }

    public Role()
    {
        UserRoles = new List<UserRole>();
    }
}
