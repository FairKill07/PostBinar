using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class NoteRepository : Repository<Note, NoteId>, INoteRepository
{
    public NoteRepository(PostBinarDbContext context) : base(context) { }

    public async Task<List<Note>> GetAllAsync(ProjectId projectId)
    {
        var notes = await _context.Notes
            .Where(n => n.ProjectId == projectId)
            .ToListAsync();
        return notes;
    }

    public void Update(Note note)
    {
        _context.Notes.Update(note);
    }
}
