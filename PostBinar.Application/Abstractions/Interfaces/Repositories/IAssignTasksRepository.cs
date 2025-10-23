using Microsoft.EntityFrameworkCore;
using PostBinar.Domain.TaskItems;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IAssignTasksRepository
{
    void Add(AssingTask assingTask);

    void Delete(AssingTask assingTask);

    Task<AssingTask?> GetByIdAsync(TaskItemId id);
}
