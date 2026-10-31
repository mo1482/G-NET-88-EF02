using EventHub.Data;
using EventHub.Models;
using Microsoft.EntityFrameworkCore;

const string connectionString =
    @"Server=(localdb)\MSSQLLocalDB;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=True;";

var options = new DbContextOptionsBuilder<EventHubDbContext>()
    .UseSqlServer(connectionString)
    .Options;

using var db = new EventHubDbContext(options);

// Creates the database and tables when they do not already exist.
db.Database.EnsureCreated();

// Add sample organizer, profile, and event once.
if (!db.Organizers.Any())
{
    var organizer = new Organizer
    {
        Name = "Ahmed Hassan",
        CompanyName = "EventHub Demo Company",
        IsVerified = true,
        Profile = new OrganizerProfile
        {
            Biography = "We organize technology and community events.",
            WebsiteUrl = "https://example.com",
            LogoUrl = "https://example.com/logo.png"
        },
        Events =
        {
            new Event
            {
                Title = "Dotnet Developers Meetup",
                Description = "A meetup for developers interested in .NET and C#.",
                StartDate = DateTime.Today.AddDays(30),
                EndDate = DateTime.Today.AddDays(30).AddHours(4),
                MaxAttendees = 100
            }
        }
    };

    db.Organizers.Add(organizer);
}

// Add a sample attendee with one VIP badge once.
if (!db.Attendees.Any())
{
    var attendee = new Attendee
    {
        FullName = "Sara Mohamed",
        Email = "sara@example.com",
        Street = "El Gomhoreya Street",
        City = "Mansoura",
        Country = "Egypt",
        PostalCode = "35511",
        Badge = new Badge
        {
            BadgeNumber = 100001,
            IssuedDate = DateTime.Today,
            Tier = BadgeTier.VIP
        }
    };

    db.Attendees.Add(attendee);
}

db.SaveChanges();

Console.WriteLine("EventHub database is ready.\n");

Console.WriteLine("Published events:");
var events = db.Events
    .Include(@event => @event.Organizer)
    .OrderBy(@event => @event.StartDate)
    .ToList();

foreach (var @event in events)
{
    Console.WriteLine($"- {@event.Title} | Organizer: {@event.Organizer.Name} | Starts: {@event.StartDate:d}");
}

Console.WriteLine("\nAttendees:");
var attendees = db.Attendees
    .Include(attendee => attendee.Badge)
    .ToList();

foreach (var attendee in attendees)
{
    var badgeInfo = attendee.Badge is null
        ? "No badge issued"
        : $"Badge #{attendee.Badge.BadgeNumber} ({attendee.Badge.Tier})";

    Console.WriteLine($"- {attendee.FullName} | {attendee.FullAddress} | {badgeInfo}");
}
