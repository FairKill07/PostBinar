using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Services;

public sealed class TasksService : ITasksService
{
    private readonly ITasksRepository _tasksRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TasksService(ITasksRepository tasksRepository, IUnitOfWork unitOfWork)
    {
        _tasksRepository = tasksRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<TaskItemId>> CreateTaskAsync(ProjectId projectId, UserId authorId, int? categoryId, string title, string? description, DateTimeOffset? deadline, TaskItemStatus status, TaskItemPriority priority)
    {
        Result<TaskItem> result = TaskItem.Create(projectId, authorId, categoryId, title, description, deadline, status, priority);
        if (result.IsFailure)
            return Result.Failure<TaskItemId>(result.Error);

        TaskItem taskItem = result.Value;
        _tasksRepository.Add(taskItem);
        
        await _unitOfWork.SaveChangesAsync();

        return taskItem.Id;
    }

    public async Task<Result> DeleteTaskAsync(TaskItem task)
    {
        _tasksRepository.Delete(task);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<List<TaskItem>> GetAllTasksAsync(ProjectId projectId)
    {
        var tasks = await _tasksRepository.GetAllTasksAsync(projectId);

        var activeTasks = tasks.Where(t => t.IsActive).ToList();

        return activeTasks;
    }

    public async Task<TaskItem?> GetTaskByIdAsync(TaskItemId taskItemId)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(taskItemId);
        
        if (task == null || !task.IsActive)
        {
            return null;
        }

        return task;
    }

    public async Task<Result> UpdateTaskAsync(
        TaskItemId taskId,
        string title,
        string? description,
        int? categoryId,
        TaskItemStatus status,
        TaskItemPriority priority,
        DateTimeOffset? deadline)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(taskId);
        if (task is null)
            return Result.Failure(TaskErrors.NotFound);

        Result result = task.Update(title, description, categoryId, status, priority, deadline);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

}
