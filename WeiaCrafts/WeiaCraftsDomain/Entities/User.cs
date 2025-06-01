using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Reflection.Metadata.BlobBuilder;

namespace WeiaCraftsDomain.Entities;

public abstract class User
{
    [Key]
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    [ForeignKey(nameof(AccountStatus))]
    public int AccountStatusId { get; set; }  
    public virtual AccountStatus AccountStatus { get; set; } = null!;

    [Required]
    public DateOnly RegisterationDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    [ForeignKey(nameof(Cart))]
    public int CartId { get; set; }
    public virtual Cart? Cart { get; set; }  
    
    public string? ProfilePictureUrl { get; set; }
    public DateTime? BirthDate { get; set; }

    [MaxLength(10)]
    public string? Gender { get; set; }
    public bool IsActive { get; set; } = true;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public abstract string GetRole();
 
}




 