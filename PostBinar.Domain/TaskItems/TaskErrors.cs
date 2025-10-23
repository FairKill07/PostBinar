using PostBinar.Domain.Abstraction;


namespace PostBinar.Domain.TaskItems;

public static class TaskErrors
{
    public static readonly NotFoundError NotFound =
        new("Task.NotFound", "The task with the specified identifier was not found");

    public static readonly Error FailedCreate =
        new("Task.FailedCreate", "Failed to create task item");

    public static readonly Error InvalidProjectId =
        new("Task.InvalidProjectId", "Project ID is required");

    public static readonly Error InvalidAuthorId =
        new("Task.InvalidAuthorId", "Author ID is required");

    public static readonly Error InvalidTitle =
        new("Task.InvalidTitle", "Title is required");

    public static readonly Error FailedUpdate =
        new("Task.FailedUpdate", "Failed to update task item");

    public static readonly Error FailedDeactivate =
        new("Task.FailedDeactivate", "Failed to deactivate task item");

    public static readonly Error AlreadyDeactivated =
        new("Task.AlreadyDeactivated", "Task is already deactivated");
}
