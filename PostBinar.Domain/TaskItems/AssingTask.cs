using PostBinar.Domain.Users;

namespace PostBinar.Domain.TaskItems;

public sealed class AssingTask
{
    public required UserId UserId { get; set; }
    public required TaskItemId TaskItemId { get; set; }
}
