using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsDomain.Entities;
public class Category
{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
