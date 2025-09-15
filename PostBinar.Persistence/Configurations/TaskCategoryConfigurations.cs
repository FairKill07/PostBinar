using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Categorys;

namespace PostBinar.Persistence.Configurations;

internal sealed class TaskCategoryConfigurations : IEntityTypeConfiguration<TaskCategory>
{
    public void Configure(EntityTypeBuilder<TaskCategory> builder)
    {
        builder
            .ToTable("task_categories");

        builder
            .HasKey(tc => tc.Id);

        builder
            .Property(tc => tc.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(tc => tc.ColorCode)
            .HasMaxLength(7)
            .IsRequired();
    }
}
