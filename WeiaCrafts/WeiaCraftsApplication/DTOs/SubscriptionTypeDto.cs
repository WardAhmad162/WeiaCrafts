namespace WeiaCraftsApplication.DTOs;

public class SubscriptionTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public int ActiveSubscriptionsCount { get; set; }
} 