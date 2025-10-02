using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface INoteService
{
    Task<NoteId> CreateAsync(
        ProjectId projectId,
        UserId authorId,
        string title,
        string? content,
        int? categoryId);
    Task UpdateAsync(
        NoteId noteId,
        string title,
        string? content,
        int? categoryId);
    Task DeleteAsync(NoteId noteId);
    Task<List<Note>> GetAllAsync(ProjectId projectId);
}
