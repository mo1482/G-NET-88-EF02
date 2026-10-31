using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models;

[Table("EventOrganizers")]
public class Organizer
{
    public int OrganizerId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(150)]
    public string? CompanyName { get; set; }

    // Becomes true after the platform's internal review is completed.
    public bool IsVerified { get; set; }

    // One organizer has one profile page.
    public OrganizerProfile Profile { get; set; } = null!;

    // One organizer can publish many events.
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
