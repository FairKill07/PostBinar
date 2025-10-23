using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface INoteService
{
    Task<Result<NoteId>> CreateAsync(
        ProjectId projectId,
        UserId authorId,
        string title,
        string? content,
        int? categoryId,
        CancellationToken cancellationToken);

    Task<Result> UpdateAsync(
        NoteId noteId,
        string title,
        string? content,
        int? categoryId,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(
        NoteId noteId,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Note>>> GetAllAsync(
        ProjectId projectId, 
        CancellationToken cancellationToken);
}
