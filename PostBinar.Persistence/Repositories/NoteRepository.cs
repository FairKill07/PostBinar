using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class NoteRepository : Repository<Note, NoteId>, INoteRepository
{
    public NoteRepository(PostBinarDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Note>> GetAllAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Note> query = _context.Notes
            .Where(n => n.ProjectId == projectId);

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
