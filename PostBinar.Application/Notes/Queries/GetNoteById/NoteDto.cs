using PostBinar.Application.Common.Mappings;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Queries.GetNoteById;

public sealed class NoteDto : IMapWith<Note>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int? CategoryId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
