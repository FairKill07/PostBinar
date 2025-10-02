using MediatR;
using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Notes.Commands.CreateNote;

public sealed record CreateNoteCommand(
    ProjectId ProjectId,
    UserId AuthorId,
    string Title,
    string? Content,
    int? CategoryId) : IRequest<NoteId>;
