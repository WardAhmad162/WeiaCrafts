using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WeiaCraftsDomain.Enum;

namespace WeiaCraftsDomain.Entities;

public class Lesson
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public int SequenceOrder { get; set; }

    [Required]
    public LessonContentType ContentType { get; set; }

    // URL,quiz whatever
    public string? ContentData { get; set; }

    [Required]
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public virtual Course Course { get; set; } = null!;
    public virtual List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual List<CourseMaterial> LessonMaterials { get; set; } = new List<CourseMaterial>();
}
