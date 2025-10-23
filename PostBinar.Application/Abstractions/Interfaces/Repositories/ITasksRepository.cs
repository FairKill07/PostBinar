using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface ITasksRepository
{
    void Add(TaskItem task);
    void Delete(TaskItem task);

    Task<TaskItem?> GetTaskByIdAsync(
        TaskItemId taskItemId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TaskItem>> GetAllTasksAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default);
}
