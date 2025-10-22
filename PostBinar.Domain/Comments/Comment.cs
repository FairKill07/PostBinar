using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Comments;

public sealed class Comment : Entity<CommentId>
{
    private Comment(
        CommentId id,
        UserId authorId,
        ProjectId projectId,
        Guid objectId,
        CommentObjectType objectType,
        string context,
        DateTimeOffset createdAt)
        : base(id)
    {
        AuthorId = authorId;
        ProjectId = projectId;
        ObjectId = objectId;
        ObjectType = objectType;
        Context = context;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
        IsActive = true;
    }

    // EF Core
    protected Comment() { }

    public UserId AuthorId { get; private set; }
    public ProjectId ProjectId { get; private set; }
    public Guid ObjectId { get; private set; }
    public CommentObjectType ObjectType { get; private set; }
    public string Context { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public static Result<Comment> Create(
        UserId authorId,
        ProjectId projectId,
        Guid objectId,
        CommentObjectType objectType,
        string context)
    {
        if (authorId == null || authorId.Value == Guid.Empty)
            return Result.Failure<Comment>(CommentErrors.InvalidAuthorId);
        if (projectId == null || projectId.Value == Guid.Empty)
            return Result.Failure<Comment>(CommentErrors.InvalidProjectId);
        if (string.IsNullOrWhiteSpace(context))
            return Result.Failure<Comment>(CommentErrors.InvalidContext);

        var comment = new Comment(
            CommentId.New(),
            authorId,
            projectId,
            objectId,
            objectType,
            context,
            DateTimeOffset.UtcNow);

        return Result.Success(comment);
    }

    public Result Update(string newContext)
    {
        Context = newContext;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure(CommentErrors.AlreadyInactive);

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure(CommentErrors.AlreadyActive);

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }
}
