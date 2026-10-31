using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Configurations;

public sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");

        builder.HasKey(@event => @event.EventId);

        builder.Property(@event => @event.Title)
            .IsRequired();

        builder.Property(@event => @event.Description)
            .IsRequired();

        builder.Property(@event => @event.StartDate)
            .IsRequired();

        builder.Property(@event => @event.EndDate)
            .IsRequired(false);

        builder.Property(@event => @event.MaxAttendees)
            .IsRequired();

        builder.HasOne(@event => @event.Organizer)
            .WithMany(organizer => organizer.Events)
            .HasForeignKey(@event => @event.OrganizerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
