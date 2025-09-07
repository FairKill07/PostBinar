using CSharpFunctionalExtensions;
using PostBinar.Domain.Categorys;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.TaskItems;

public sealed class TaskItem : Abstraction.Entity<TaskItemId>
{
    private TaskItem(
        TaskItemId id,
        ProjectId projectId,
        UserId authorId,
        int? categoryId,
        string title,
        string? description,
        DateTimeOffset? deadline,
        TaskItemStatus status,
        TaskItemPriority priority,
        DateTimeOffset createdAt)
        : base(id)
    {
        ProjectId = projectId;
        AuthorId = authorId;
        CategoryId = categoryId;
        Title = title;
        Description = description;
        Deadline = deadline;
        Status = status;
        Priority = priority;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
        IsActive = true;
    }

    protected TaskItem() { } // EF Core

    public ProjectId ProjectId { get; private set; }
    public UserId AuthorId { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public TaskItemStatus Status { get; private set; }
    public int? CategoryId { get; private set; }
    public TaskItemPriority Priority { get; private set; }
    public DateTimeOffset? Deadline { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public static Result<TaskItem> Create(
        ProjectId projectId,
        UserId authorId,
        int? categoryId,
        string title,
        string? description,
        DateTimeOffset? deadline,
        TaskItemStatus status,
        TaskItemPriority priority,
        DateTimeOffset createdAt)
    {
        if (projectId == null || projectId.Value == Guid.Empty)
            return Result.Failure<TaskItem>("Project ID is required");
        if (authorId == null || authorId.Value == Guid.Empty)
            return Result.Failure<TaskItem>("Author ID is required");
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<TaskItem>("Title is required");

        var taskItem = new TaskItem(
            TaskItemId.New(),
            projectId,
            authorId,
            categoryId,
            title,
            description,
            deadline,
            status,
            priority,
            createdAt);

        return Result.Success(taskItem);
    }

    public Result Update(
        string title,
        string? description,
        int? categoryId,
        TaskItemStatus status,
        TaskItemPriority priority,
        DateTimeOffset? deadline)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure("Title is required");

        Title = title;
        Description = description;
        CategoryId = categoryId;
        Status = status;
        Priority = priority;
        Deadline = deadline;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure("Task is already deactivated");

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }
}
