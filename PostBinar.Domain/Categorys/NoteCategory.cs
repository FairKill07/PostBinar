namespace PostBinar.Domain.Categorys;

public sealed class NoteCategory 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ColorCode { get; set; } = "#FFFFFF";
}
