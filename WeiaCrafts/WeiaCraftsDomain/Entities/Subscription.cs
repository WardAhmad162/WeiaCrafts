using WeiaCraftsDomain.Enum;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsDomain.Entities;

public class Subscription
{
    public int Id { get; set; }

    public int SubscriptionTypeId { get; set; }
    public SubscriptionType SubscriptionType { get; set; }

    public SubscriptionStatus Status { get; set; } 

    public AmountWithCurrency Price { get; set; }

    public DateTime SubscriptionStartDate { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }

    public string PaidAccountTraineeUserName { get; set; }
    public PaidAccountTrainee PaidAccountTrainee { get; set; }
}
