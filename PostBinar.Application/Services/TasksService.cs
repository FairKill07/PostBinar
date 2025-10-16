using CSharpFunctionalExtensions;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
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
    public async Task<TaskItem> CreateTaskAsync(ProjectId projectId, UserId authorId, int? categoryId, string title, string? description, DateTimeOffset? deadline, TaskItemStatus status, TaskItemPriority priority)
    {
        var task =  TaskItem.Create(projectId, authorId, categoryId, title, description, deadline, status, priority);
        
        if (task.IsFailure)
        {
            throw new InvalidOperationException(task.Error);
        }
        _tasksRepository.Add(task.Value);
        
        await _unitOfWork.SaveChangesAsync();

        return task.Value;
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
            return Result.Failure("Task not found");

        var updateResult = task.Update(title, description, categoryId, status, priority, deadline);
        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        _tasksRepository.Update(task);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

}
