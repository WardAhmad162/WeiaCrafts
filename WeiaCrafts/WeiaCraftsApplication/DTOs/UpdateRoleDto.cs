using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsApplication.DTOs;

public class UpdateRoleDto
{
    [StringLength(50, MinimumLength = 2)]
    public string? Name { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }

    public bool? IsDefault { get; set; }
} 