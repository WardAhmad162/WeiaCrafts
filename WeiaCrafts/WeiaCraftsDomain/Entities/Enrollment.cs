using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeiaCraftsDomain.Enum;

namespace WeiaCraftsDomain.Entities;

public class Enrollment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    [Required]
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public virtual Course Course { get; set; } = null!;

    [Required]
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    [Required]
    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.NotStarted;

    [Required]
    [Range(0, 100)]
    public double PercentageCompleted { get; set; } = 0.0;

    public int? LastAccessedLessonId { get; set; }
    [ForeignKey(nameof(LastAccessedLessonId))]
    public virtual Lesson? LastAccessedLesson { get; set; }

    public DateTime? CompletionDate { get; set; }
}

