using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsApplication.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage ="User Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 100 characters.")]
    public string UserName { get; set; } = string.Empty;

    [Required (ErrorMessage = "Email is required")]
    [EmailAddress (ErrorMessage = "Invalid Email Address")]
    [StringLength(255, ErrorMessage = "Email cannot be longer than 255 characters")]
    public string Email { get; set; } = string.Empty;

    
    [Required]
    [StringLength(10)]
    public string Gender { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}


