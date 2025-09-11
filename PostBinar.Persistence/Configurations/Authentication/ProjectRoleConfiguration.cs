using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Authorization;


internal sealed class ProjectMembershipRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.ToTable("project_roles");

        builder.HasKey(x => new { x.ProjectMembershipId, x.RoleId });

        builder
            .HasOne(x => x.ProjectMembership)
            .WithMany(m => m.Roles)
            .HasForeignKey(x => x.ProjectMembershipId);

        builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}
