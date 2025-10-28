using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.TaskItems;

public sealed class TaskMember
{
    private TaskMember(
        TaskItemId taskId,
        UserId userId)
    {
        TaskId = taskId;
        UserId = userId;
    }
    public TaskItemId TaskId { get; private set; }
    public UserId UserId { get; private set; }
    
    public static Result<TaskMember> Create(
        TaskItemId taskId,
        UserId userId)
    {
        if (taskId == TaskItemId.Empty)
        {
            return Result.Failure<TaskMember>(Error.NullValue);
        }
        if (userId == UserId.Empty)
        {
            return Result.Failure<TaskMember>(Error.NullValue);
        }
        
        var taskMember = new TaskMember(
            taskId,
            userId);

        return taskMember;
    }
}
