using System.ComponentModel.DataAnnotations.Schema;
using WeiaCraftsDomain.Entities;

public class CourseFeedback : Feedback
{
    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;
}
