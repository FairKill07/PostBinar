using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
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
            .HasMaxLength(300);
        
        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(10000);

        builder
            .Property(p => p.OwnerId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder
            .Property(p => p.CreatedAt)
            .IsRequired();
        
        builder
            .Property(p => p.UpdatedAt)
            .IsRequired();

        builder
            .Property(p => p.IsActive)
            .IsRequired();

        builder
            .HasMany(p => p.ProjectMemberships)
            .WithOne(m => m.Project)
            .HasForeignKey(pm => pm.ProjectId);

        builder
            .HasMany(p => p.Tasks)
            .WithOne() 
            .HasForeignKey(t => t.ProjectId);

        builder
            .HasMany(p => p.Notes)
            .WithOne()
            .HasForeignKey(n => n.ProjectId);

        
        

        builder.HasIndex(p => p.IsActive);
        builder.HasIndex(p => p.OwnerId);
    }
}
