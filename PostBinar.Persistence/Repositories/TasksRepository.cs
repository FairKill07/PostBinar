using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class TasksRepository : Repository<TaskItem, TaskItemId>, ITasksRepository
{
    public TasksRepository(PostBinarDbContext context) : base(context) { }

    public async Task<IReadOnlyList<TaskItem>> GetAllTasksAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TaskItem> query = _context.TaskItems
            .Where(ti => ti.ProjectId == projectId);

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskItem?> GetTaskByIdAsync(
        TaskItemId taskItemId,
        CancellationToken cancellationToken = default)
    {
        return await _context.TaskItems
            .AsNoTracking()
            .FirstOrDefaultAsync(ti => ti.Id == taskItemId, cancellationToken);
    }
}
