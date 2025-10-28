using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.TaskItems;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class TaskMemberRepository : ITasksMemberRepository
{
    private readonly PostBinarDbContext _context;

    public TaskMemberRepository(PostBinarDbContext context) 
    {
        _context = context;
    }

    public void Add(TaskMember taskMember)
    {
        _context.TaskMembers.Add(taskMember);
    }

    public void Delete(TaskMember taskMember)
    {
        _context.TaskMembers.Remove(taskMember);
    }

    public async Task<IReadOnlyList<TaskMember>> GetAllByIdAsync(TaskItemId id, CancellationToken cancellationToken)
    {
        IQueryable<TaskMember> query = _context.TaskMembers
            .Where(x => x.TaskId == id);

        var list = await query.ToListAsync(cancellationToken);
        return list;
    }
}
