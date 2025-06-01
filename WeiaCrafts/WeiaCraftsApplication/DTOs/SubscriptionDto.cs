using WeiaCraftsDomain.Enum;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsApplication.DTOs;

public class SubscriptionDto
{
    public int Id { get; set; }
    public int SubscriptionTypeId { get; set; }
    public string SubscriptionTypeName { get; set; } = string.Empty;
    public SubscriptionStatus Status { get; set; }
    public AmountWithCurrency Price { get; set; }
    public DateTime SubscriptionStartDate { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }
    public string PaidAccountTraineeUserName { get; set; } = string.Empty;
} 