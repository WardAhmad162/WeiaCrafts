using System.ComponentModel.DataAnnotations;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsDomain.Entities;

public class Cart
{
    [Key]
    public int Id { get; set; }

    public required string UserName { get; set; }
    public required User User { get; set; }

    public AmountWithCurrency TotalAmount { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); 
}



