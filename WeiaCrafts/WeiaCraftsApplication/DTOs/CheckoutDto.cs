namespace WeiaCraftsApplication.DTOs;

public class CheckoutDto
{
    public int UserId { get; set; }
    public List<int> ProductIds { get; set; }
    public string PaymentMethod { get; set; }
}
