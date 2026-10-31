namespace EventHub.Models;

public enum BadgeTier
{
    Standard = 1,
    VIP = 2
}

public class Badge
{
    public int BadgeId { get; set; }

    public int AttendeeId { get; set; }

    // Unique badge number across the platform.
    public int BadgeNumber { get; set; }

    public DateTime IssuedDate { get; set; }

    public BadgeTier Tier { get; set; }

    public Attendee Attendee { get; set; } = null!;
}
