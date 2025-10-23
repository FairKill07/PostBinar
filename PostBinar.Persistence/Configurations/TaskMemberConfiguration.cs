using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Configurations;

internal sealed class TaskMemberConfiguration : IEntityTypeConfiguration<TaskMember>
{
    public void Configure(EntityTypeBuilder<TaskMember> builder)
    {
        builder.ToTable("tasks_member");

        builder.HasKey(x => new { x.TaskId, x.UserId });

        builder
            .Property(tm => tm.UserId)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder
            .Property(tm => tm.TaskId)
            .HasConversion(id => id.Value, value => new TaskItemId(value));

        builder
            .HasOne<TaskItem>()
            .WithMany()
            .HasForeignKey(tm => tm.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(tm => tm.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
