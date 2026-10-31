using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Configurations;

public sealed class OrganizerProfileConfiguration
    : IEntityTypeConfiguration<OrganizerProfile>
{
    public void Configure(EntityTypeBuilder<OrganizerProfile> builder)
    {
        builder.ToTable("OrganizerProfiles");

        // OrganizerId is both the profile's PK and its FK.
        builder.HasKey(profile => profile.OrganizerId);

        builder.Property(profile => profile.Biography)
            .IsRequired(false);

        builder.Property(profile => profile.WebsiteUrl)
            .IsRequired(false);

        builder.Property(profile => profile.LogoUrl)
            .IsRequired(false);

        builder.HasOne(profile => profile.Organizer)
            .WithOne(organizer => organizer.Profile)
            .HasForeignKey<OrganizerProfile>(profile => profile.OrganizerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
