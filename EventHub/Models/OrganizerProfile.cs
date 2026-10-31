namespace EventHub.Models;

public class OrganizerProfile
{
    // Shared primary key: also the required foreign key to Organizer.
    public int OrganizerId { get; set; }

    public string? Biography { get; set; }

    public string? WebsiteUrl { get; set; }

    // Store the uploaded logo's path or URL.
    public string? LogoUrl { get; set; }

    public Organizer Organizer { get; set; } = null!;
}
