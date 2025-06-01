using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeiaCraftsDomain.Enum;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsDomain.Entities;

public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public CourseLevel Level { get; set; }
    public CourseAccessType AccessType { get; set; }
    public CourseStatus Status { get; set; }
    public CourseLanguage Language { get; set; }

    public bool IsKidFriendly { get; set; }

    public AmountWithCurrency Price { get; set; }

    public double Rating { get; set; }
    public int RatingCount { get; set; }

    public string? CoverImageURL { get; set; }
    public string? PromoVideoURL { get; set; }

    public string? TrainerUserName { get; set; }
    [ForeignKey(nameof(TrainerUserName))]
    public virtual Trainer? Trainer { get; set; }

    public WideRangeCategory WideRangeCategory { get; set; }

    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedTimestamp { get; set; } = DateTime.UtcNow;

    public virtual List<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual List<CourseFeedback> FeedbacksRecieved { get; set; } = new List<CourseFeedback>();
    public virtual List<CourseRating> RatingsReceived { get; set; } = new List<CourseRating>();
    public virtual List<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
    public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Trainee> EnrolledTrainees { get; set; } = new List<Trainee>();
    public virtual ICollection<Trainee> CompletedTrainees { get; set; } = new List<Trainee>();
    public virtual ICollection<Trainee> FavoritedByTrainees { get; set; } = new List<Trainee>();
}
