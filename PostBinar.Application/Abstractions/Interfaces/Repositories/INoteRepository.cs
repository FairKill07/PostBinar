using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface INoteRepository
{
    void Add(Note note);
    void Delete(Note note);
    
    Task<Note?> GetByIdAsync(
        NoteId id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Note>> GetAllAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default);
}
