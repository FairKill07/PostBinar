using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.TaskItems;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class AssignTasksRepository : IAssignTasksRepository
{
    private readonly PostBinarDbContext _context;

    public AssignTasksRepository(PostBinarDbContext context) 
    {
        _context = context;
    }

    public void Add(AssingTask assingTask)
    {
        _context.AssingTasks.Add(assingTask);
    }

    public void Delete(AssingTask assingTask)
    {
        _context.AssingTasks.Remove(assingTask);
    }

    public async Task<AssingTask?> GetByIdAsync(TaskItemId id)
    {
        return await _context.AssingTasks.FindAsync(id);
    }
}
