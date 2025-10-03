using MediatR;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Queries.GetNoteById;

public sealed record GetNoteByIdQuery(NoteId NoteId) : IRequest<NoteDto>
{
}
