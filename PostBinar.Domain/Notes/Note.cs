using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Comments;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Notes;

public sealed class Note : Entity<NoteId>
{
    private readonly List<Comment> _comments = [];
    private Note(
        NoteId id,
        ProjectId projectId,
        UserId authorId,
        string title,
        string? content,
        int? categoryId,
        DateTimeOffset createdAt)
        : base(id)
    {
        ProjectId = projectId;
        AuthorId = authorId;
        Title = title;
        Content = content;
        CategoryId = categoryId;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
        IsActive = true;
    }

    // EF Core
    protected Note() { }

    public ProjectId ProjectId { get; private set; }
    public UserId AuthorId { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Content { get; private set; }
    public int? CategoryId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public IReadOnlyCollection<Comment> Comments => _comments;

    public static Result<Note> Create(
        ProjectId projectId,
        UserId authorId,
        string title,
        string? content,
        int? categoryId)
    {
        if (projectId == null || projectId.Value == Guid.Empty)
            return Result.Failure<Note>(NoteErrors.InvalidProjectId);
        if (authorId == null || authorId.Value == Guid.Empty)
            return Result.Failure<Note>(NoteErrors.InvalidAuthorId);
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<Note>(NoteErrors.InvalidTitle);

        var now = DateTimeOffset.UtcNow;
        var note = new Note(
            NoteId.New(),
            projectId,
            authorId,
            title,
            content,
            categoryId,
            now);

        return note;
    }

    public Result Update(
        string title,
        string? content,
        int? categoryId)
    {
        Title = title;
        Content = content;
        CategoryId = categoryId;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure(NoteErrors.AlreadyInactive);

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure(NoteErrors.AlreadyActive);

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }
}
