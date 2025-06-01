using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsApplication.DTOs;

public class UpdateProfileDto
{
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [StringLength(255, ErrorMessage = "Email cannot be longer than 255 characters")]
    public string? Email { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    public string? ProfilePictureUrl { get; set; }
}
