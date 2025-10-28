using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public  interface ITasksService
{
    Task<Result<TaskItem?>> GetTaskByIdAsync(
        TaskItemId taskItemId,
        CancellationToken cancellationToken);
    
    Task<Result<IReadOnlyList<TaskItem>>> GetAllTasksAsync(
        ProjectId projectId,
        CancellationToken cancellationToken);
   
    Task<Result<TaskItemId>> CreateTaskAsync(
        ProjectId projectId, 
        UserId authorId, 
        int? categoryId, 
        string title, 
        string? description,
        DateTimeOffset? deadline,
        TaskItemStatus status,
        TaskItemPriority priority,
        CancellationToken cancellationToken);
    
    Task<Result> UpdateTaskAsync(
        TaskItemId taskId,
        string title,
        string? description,
        int? categoryId,
        TaskItemStatus status,
        TaskItemPriority priority,
        DateTimeOffset? deadline,
        CancellationToken cancellationToken);
    
    Task<Result> DeleteTaskAsync(
        TaskItem taskId,
        CancellationToken cancellationToken);

    Task<Result> AssignTaskToUserAsync(
        TaskItemId taskId,
        UserId userId,
        CancellationToken cancellationToken);
}
