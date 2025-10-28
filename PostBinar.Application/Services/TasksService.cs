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
    private readonly ITasksMemberRepository _tasksMemberRepository;

    public TasksService(ITasksRepository tasksRepository, IUnitOfWork unitOfWork, ITasksMemberRepository tasksMemberRepository)
    {
        _tasksRepository = tasksRepository;
        _unitOfWork = unitOfWork;
        _tasksMemberRepository = tasksMemberRepository;
    }

    public async Task<Result<TaskItemId>> CreateTaskAsync(
        ProjectId projectId,
        UserId authorId,
        int? categoryId,
        string title,
        string? description,
        DateTimeOffset? deadline,
        TaskItemStatus status,
        TaskItemPriority priority,
        CancellationToken cancellationToken)
    {
        var result = TaskItem.Create(projectId, authorId, categoryId, title, description, deadline, status, priority);
        if (result.IsFailure)
            return Result.Failure<TaskItemId>(result.Error);

        var taskItem = result.Value;

        _tasksRepository.Add(taskItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return taskItem.Id;
    }

    public async Task<Result> DeleteTaskAsync(TaskItem task, CancellationToken cancellationToken)
    {
        if (task is null)
            return Result.Failure(TaskErrors.NotFound);

        _tasksRepository.Delete(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<TaskItem>>> GetAllTasksAsync(
        ProjectId projectId,
        CancellationToken cancellationToken)
    {
        var tasks = await _tasksRepository.GetAllTasksAsync(projectId, cancellationToken);
        
        var activeTasks = tasks.Where(t => t.IsActive).ToList();

        return activeTasks;
    }

    public async Task<Result<TaskItem?>> GetTaskByIdAsync(
        TaskItemId taskItemId,
        CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(taskItemId, cancellationToken);

        if (task is null || !task.IsActive)
            return Result.Failure<TaskItem?>(TaskErrors.NotFound);

        return task;
    }

    public async Task<Result> UpdateTaskAsync(
        TaskItemId taskId,
        string title,
        string? description,
        int? categoryId,
        TaskItemStatus status,
        TaskItemPriority priority,
        DateTimeOffset? deadline,
        CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(taskId, cancellationToken);
        if (task is null)
            return Result.Failure(TaskErrors.NotFound);

        var updateResult = task.Update(title, description, categoryId, status, priority, deadline);
        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> AssignTaskToUserAsync(
        TaskItemId taskId,
        UserId userId,
        CancellationToken cancellationToken)
    {
        var result = TaskMember.Create(taskId, userId);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        var taskAssing = result.Value;

        _tasksMemberRepository.Add(taskAssing);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
