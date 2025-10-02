using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface INoteRepository
{
    void Add(Note note);
    void Delete(Note note);
    void Update(Note note);
    Task<Note?> GetByIdAsync(NoteId id);
    Task<List<Note>> GetAllAsync(ProjectId projectId);
}
