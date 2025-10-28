using PostBinar.Domain.TaskItems;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface ITasksMemberRepository
{
    void Add(TaskMember assingTask);

    void Delete(TaskMember assingTask);

    Task<IReadOnlyList<TaskMember>> GetAllByIdAsync(TaskItemId id, CancellationToken cancellationToken);
}
