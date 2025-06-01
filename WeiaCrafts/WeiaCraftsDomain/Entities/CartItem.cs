using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsDomain.Entities;

public class CartItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Cart))]
    public int CartId { get; set; }
    public virtual Cart Cart { get; set; } = null!;

    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;

    public int Quantity { get; set; }
    public AmountWithCurrency Price { get; set; } 
}
