using MediatR;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Commands.DeleteNote;

public sealed record DeleteNoteCommand(NoteId NoteId) : IRequest<bool>;
