using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.TaskItems
{
    public sealed class TaskItem : Abstraction.Entity<TaskItemId>
    {
        public ProjectId ProjectId { get; set; }
        public UserId AuthorId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskItemStatus Status { get; set; }
        public int? CategoryId { get; set; }
        public TaskItemPriority Priority { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Navigation properties
        public User Author { get; set; } = null!;
        public Project Project { get; set; } = null!;
        public TaskCategory? Category { get; set; }
    }
}
