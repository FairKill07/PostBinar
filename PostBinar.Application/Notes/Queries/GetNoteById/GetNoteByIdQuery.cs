using MediatR;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Queries.GetNoteById;

public sealed record GetNoteByIdQuery(NoteId NoteId) : IRequest<Result<NoteDto>>;
