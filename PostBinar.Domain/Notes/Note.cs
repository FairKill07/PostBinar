using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Notes;

public sealed class Note : Abstraction.Entity<NoteId>
{
    public ProjectId ProjectId { get; set; }
    public UserId AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; } 

    // Navigation properties
    public Project Project { get; set; } = null!;
    public User Author { get; set; } = null!;
    public NoteCategory? Category { get; set; }

}
