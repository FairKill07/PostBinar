using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Configurations;

internal sealed class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("task_items");
        
        builder
            .HasKey(t => t.Id);
        
        builder
            .Property(t => t.Id)
            .HasConversion(id => id.Value, value => new TaskItemId(value))
            .ValueGeneratedNever();
        
        builder
            .Property(t => t.ProjectId)
            .HasConversion(id => id.Value, value => new ProjectId(value))
            .IsRequired();
        
        builder
            .Property(t => t.AuthorId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder
            .Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(300);
       
        builder
            .Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder
            .Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();

        builder
            .Property(t => t.CategoryId)
            .IsRequired(false);

        builder
            .Property(t => t.Priority)
            .IsRequired()
            .HasConversion<string>();
        
        builder
            .Property(t => t.Deadline)
            .IsRequired(false);
        
        builder
            .Property(t => t.CreatedAt)
            .IsRequired();
        
        builder
            .Property(t => t.UpdatedAt)
            .IsRequired();

        builder
            .HasOne<Project>()
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
