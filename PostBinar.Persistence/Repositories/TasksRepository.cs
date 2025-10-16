using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal class TasksRepository : Repository<TaskItem, TaskItemId> ,ITasksRepository 
{
    public TasksRepository(PostBinarDbContext context) : base(context)
    {
    }

    public async Task<List<TaskItem>> GetAllTasksAsync(ProjectId projectId)
    {
        return await _context.TaskItems.Where(ti => ti.ProjectId == projectId).ToListAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(TaskItemId taskItemId)
    {
        return await _context.TaskItems.FirstOrDefaultAsync(ti => ti.Id == taskItemId);
    }

    public void Update(TaskItem task)
    {
        _context.TaskItems.Update(task);
    }
}
