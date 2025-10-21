using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;


namespace PostBinar.Persistence.Configurations;

internal sealed class AssingTaskConfiguration : IEntityTypeConfiguration<AssingTask>
{
    public void Configure(EntityTypeBuilder<AssingTask> builder)
    {
        builder.ToTable("assing_tasks");

        builder
            .HasKey(x => new {x.TaskItemId});

        builder
            .Property(at => at.UserId)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder
            .Property(at => at.TaskItemId)
            .HasConversion(id => id.Value, value => new TaskItemId(value));
    }
}
