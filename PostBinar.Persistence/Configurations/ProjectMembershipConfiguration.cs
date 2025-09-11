using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Configurations;

internal sealed class ProjectMembershipConfiguration : IEntityTypeConfiguration<ProjectMembership>
{
    public void Configure(EntityTypeBuilder<ProjectMembership> builder)
    {
        builder.ToTable("project_memberships");

        builder
            .HasKey(pm => pm.Id);

        builder
            .Property(pm => pm.Id)
            .HasConversion(id => id.Value, value => new ProjectMembershipId(value))
            .ValueGeneratedNever();

        builder
            .Property(pm => pm.UserId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder
            .Property(pm => pm.ProjectId)
            .HasConversion(id => id.Value, value => new ProjectId(value))
            .IsRequired();

        builder
            .Property(pm => pm.JoinedAt)
            .IsRequired();
    }
}
