using CSharpFunctionalExtensions;
using PostBinar.Domain.Categorys;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Notes;

public sealed class Note : Abstraction.Entity<NoteId>
{
    private Note(
        NoteId id,
        ProjectId projectId,
        UserId authorId,
        string title,
        string? description,
        int? categoryId,
        DateTimeOffset createdAt)
        : base(id)
    {
        ProjectId = projectId;
        AuthorId = authorId;
        Title = title;
        Description = description;
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
    public string? Description { get; private set; }
    public int? CategoryId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public static Result<Note> Create(
        ProjectId projectId,
        UserId authorId,
        string title,
        string? description,
        int? categoryId)
    {
        if (projectId == null || projectId.Value == Guid.Empty)
            return Result.Failure<Note>("Project ID is required");
        if (authorId == null || authorId.Value == Guid.Empty)
            return Result.Failure<Note>("Author ID is required");
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<Note>("Title is required");

        var now = DateTimeOffset.UtcNow;
        var note = new Note(
            NoteId.New(),
            projectId,
            authorId,
            title,
            description,
            categoryId,
            now);

        return Result.Success(note);
    }

    public Result Update(
        string title,
        string? description,
        int? categoryId)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure("Title is required");

        Title = title;
        Description = description;
        CategoryId = categoryId;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure("Note is already inactive");

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure("Note is already active");

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }
}
