using CSharpFunctionalExtensions;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public  interface ITasksService
{
    Task<TaskItem?> GetTaskByIdAsync(TaskItemId taskItemId);
    
    Task<List<TaskItem>> GetAllTasksAsync(ProjectId projectId);
   
    Task<TaskItem> CreateTaskAsync(
        ProjectId projectId, 
        UserId authorId, 
        int? categoryId, 
        string title, 
        string? description,
        DateTimeOffset? deadline,
        TaskItemStatus status,
        TaskItemPriority priority);
    
    Task<Result> UpdateTaskAsync(
        TaskItemId taskId,
        string title,
        string? description,
        int? categoryId,
        TaskItemStatus status,
        TaskItemPriority priority,
        DateTimeOffset? deadline);
    
    Task<Result> DeleteTaskAsync(TaskItem taskId);
}
