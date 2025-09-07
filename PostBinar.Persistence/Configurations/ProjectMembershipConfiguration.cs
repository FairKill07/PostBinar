using Microsoft.EntityFrameworkCore;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Configurations;

internal sealed class ProjectMembershipConfiguration : IEntityTypeConfiguration<ProjectMembership>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProjectMembership> builder)
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
            .HasMany(r => r.Roles)
            .WithOne()
            .HasForeignKey(re => re.Id)
            .IsRequired();
        
        builder
            .HasOne(pm => pm.Project)
            .WithMany(p => p.ProjectMemberships)
            .HasForeignKey(pm => pm.ProjectId);

        builder
            .HasOne(pm => pm.User)
            .WithMany(u => u.ProjectMemberships)
            .HasForeignKey(pm => pm.UserId);
    }
}
