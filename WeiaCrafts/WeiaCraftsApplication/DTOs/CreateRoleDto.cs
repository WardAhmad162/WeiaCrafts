using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsApplication.DTOs;

public class CreateRoleDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [StringLength(256)]
    public string? Description { get; set; }

    public bool IsDefault { get; set; }
    public bool IsSystem { get; set; }
} 