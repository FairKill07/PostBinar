using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface ITasksRepository
{
    Task<TaskItem?> GetTaskByIdAsync(TaskItemId taskItemId);

    Task<List<TaskItem>> GetAllTasksAsync(ProjectId projectId);

    void Add(TaskItem task);

    void Update(TaskItem task);

    void Delete(TaskItem task);
}
