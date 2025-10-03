using MediatR;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Commands.UpdateNote;

public sealed record UpdateNoteCommand(
    NoteId NoteId, 
    string Title, 
    string? Content, 
    int? CategoryId) : IRequest<Unit>;
