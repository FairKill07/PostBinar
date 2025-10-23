using MediatR;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Commands.DeleteNote;

public sealed record DeleteNoteCommand(
    NoteId NoteId,
    CancellationToken CancellationToken) : IRequest<Result>;
