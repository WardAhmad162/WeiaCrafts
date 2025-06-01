namespace WeiaCraftsApplication.DTOs;

public class CartDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = "ISL";
    public List<CartItemDto> Items { get; set; } = new();
}
