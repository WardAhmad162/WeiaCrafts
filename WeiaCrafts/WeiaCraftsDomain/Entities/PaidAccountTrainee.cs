using System.ComponentModel.DataAnnotations.Schema;
using WeiaCraftsDomain.Enum;

namespace WeiaCraftsDomain.Entities;

public class PaidAccountTrainee : Trainee
{
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public int? CurrentSubscriptionId { get; set; }
    [ForeignKey(nameof(CurrentSubscriptionId))]
    public virtual Subscription? CurrentSubscription { get; set; }

    public override string GetRole() => "Paid Account Trainee ";
}
