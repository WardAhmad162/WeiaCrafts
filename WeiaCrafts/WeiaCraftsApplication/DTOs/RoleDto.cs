namespace WeiaCraftsApplication.DTOs;

public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDefault { get; set; }
    public bool IsSystem { get; set; }
    public int UsersCount { get; set; }
} 