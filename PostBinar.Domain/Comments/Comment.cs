using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Comments;

public sealed class Comment : Abstraction.Entity<CommentId>
{
    public UserId AuthorId { get; set; }
    public ProjectId ProjectId { get; set; }
    public CommentObjectType ObjectType { get; set; }
    public string Context { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }

    // Navigation properties
    public User Author { get; set; } = null!;
    public Project Project { get; set; } = null!;
}
