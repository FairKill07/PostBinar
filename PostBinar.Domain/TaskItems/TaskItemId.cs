namespace PostBinar.Domain.TaskItems;

public record  TaskItemId (Guid Value)
{
    public static TaskItemId New() => new(Guid.NewGuid());
    public static TaskItemId Empty => new(Guid.Empty);
}
