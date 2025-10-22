using PostBinar.Domain.Abstraction;

namespace PostBinar.Domain.Notes;

public static class NoteErrors
{
    public static readonly NotFoundError NotFound =
        new("Note.NotFound", "The note with the specified identifier was not found");

    public static readonly Error InvalidProjectId =
        new("Note.InvalidProjectId", "Project ID is required");

    public static readonly Error InvalidAuthorId =
        new("Note.InvalidAuthorId", "Author ID is required");

    public static readonly Error InvalidTitle =
        new("Note.InvalidTitle", "Title is required");

    public static readonly Error FailedCreate =
        new("Note.FailedCreate", "Failed to create note");

    public static readonly Error FailedUpdate =
        new("Note.FailedUpdate", "Failed to update note");

    public static readonly Error AlreadyInactive =
        new("Note.AlreadyInactive", "Note is already inactive");

    public static readonly Error AlreadyActive =
        new("Note.AlreadyActive", "Note is already active");
}

