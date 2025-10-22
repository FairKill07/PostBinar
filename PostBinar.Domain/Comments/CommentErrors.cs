using PostBinar.Domain.Abstraction;

namespace PostBinar.Domain.Comments;

public static class CommentErrors
{
    public static readonly NotFoundError NotFound =
        new("Comment.NotFound", "The comment with the specified identifier was not found");

    public static readonly Error InvalidAuthorId =
        new("Comment.InvalidAuthorId", "Author ID is required");

    public static readonly Error InvalidProjectId =
        new("Comment.InvalidProjectId", "Project ID is required");

    public static readonly Error InvalidObjectId =
        new("Comment.InvalidObjectId", "Object ID is required");

    public static readonly Error InvalidContext =
        new("Comment.InvalidContext", "Comment text cannot be empty");

    public static readonly Error FailedCreate =
        new("Comment.FailedCreate", "Failed to create comment");

    public static readonly Error FailedUpdate =
        new("Comment.FailedUpdate", "Failed to update comment");

    public static readonly Error AlreadyInactive =
        new("Comment.AlreadyInactive", "Comment is already inactive");

    public static readonly Error AlreadyActive =
        new("Comment.AlreadyActive", "Comment is already active");
}

