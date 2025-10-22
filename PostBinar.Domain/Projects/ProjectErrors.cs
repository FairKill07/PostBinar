using PostBinar.Domain.Abstraction;

namespace PostBinar.Domain.Projects;

public static class ProjectErrors
{
    public static readonly NotFoundError NotFound =
        new("Project.NotFound", "The project with the specified identifier was not found");

    public static readonly Error InvalidName =
        new("Project.InvalidName", "Project name is required");

    public static readonly Error InvalidDescription =
        new("Project.InvalidDescription", "Project description is required");

    public static readonly Error InvalidOwnerId =
        new("Project.InvalidOwnerId", "Owner ID is required");

    public static readonly Error FailedCreate =
        new("Project.FailedCreate", "Failed to create project");

    public static readonly Error FailedUpdate =
        new("Project.FailedUpdate", "Failed to update project details");

    public static readonly Error UserAlreadyMember =
        new("Project.UserAlreadyMember", "User is already a member of this project");

    public static readonly Error UserNotMember =
        new("Project.UserNotMember", "User is not a member of this project");

    public static readonly Error CannotRemoveOwner =
        new("Project.CannotRemoveOwner", "Cannot remove project owner");

    public static readonly Error AlreadyInactive =
        new("Project.AlreadyInactive", "Project is already inactive");

    public static readonly Error AlreadyActive =
        new("Project.AlreadyActive", "Project is already active");
}

