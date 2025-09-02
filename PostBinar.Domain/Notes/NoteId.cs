namespace PostBinar.Domain.Notes;

public record NoteId (Guid Value)
{
    public static NoteId New() => new(Guid.NewGuid());
    public static NoteId Empty => new(Guid.Empty);
}
