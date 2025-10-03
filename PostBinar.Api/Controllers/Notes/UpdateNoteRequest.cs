using PostBinar.Domain.Notes;

namespace PostBinar.Api.Controllers.Notes;

public sealed record UpdateNoteRequest(
    NoteId NoteId,
    string Title,
    string? Content,
    int? CategoryId);
