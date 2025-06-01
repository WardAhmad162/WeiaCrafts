using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsDomain.Entities;

public class SubscriptionType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
}



