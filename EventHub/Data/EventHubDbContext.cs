using EventHub.Configurations;
using EventHub.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Data;

public class EventHubDbContext : DbContext
{
    public EventHubDbContext(DbContextOptions<EventHubDbContext> options)
        : base(options)
    {
    }

    public DbSet<Organizer> Organizers => Set<Organizer>();
    public DbSet<OrganizerProfile> OrganizerProfiles => Set<OrganizerProfile>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Attendee> Attendees => Set<Attendee>();
    public DbSet<Badge> Badges => Set<Badge>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API using separate configuration classes.
        modelBuilder.ApplyConfiguration(new OrganizerProfileConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());

        // Fluent API configured directly inside OnModelCreating.
        modelBuilder.Entity<Badge>(builder =>
        {
            builder.ToTable("Badges");

            builder.HasKey(badge => badge.BadgeId);

            builder.Property(badge => badge.BadgeNumber)
                .IsRequired();

            // Badge numbers must be unique across all attendees.
            builder.HasIndex(badge => badge.BadgeNumber)
                .IsUnique();

            builder.Property(badge => badge.IssuedDate)
                .IsRequired();

            builder.Property(badge => badge.Tier)
                .HasConversion<string>()
                .HasMaxLength(8)
                .IsRequired();

            builder.HasOne(badge => badge.Attendee)
                .WithOne(attendee => attendee.Badge)
                .HasForeignKey<Badge>(badge => badge.AttendeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
