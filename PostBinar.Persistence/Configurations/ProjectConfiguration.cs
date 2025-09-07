using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Projects;

namespace PostBinar.Persistence.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");
        builder
            .HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, value => new ProjectId(value));
        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);
        builder
            .Property(p => p.CreatedAt)
            .IsRequired();
        builder
            .Property(p => p.UpdatedAt)
            .IsRequired(false);

        builder
    }
}
