using CSharpFunctionalExtensions;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.ProjectsMemberships;

public sealed class ProjectMembership : Abstraction.Entity<ProjectMembershipId>
{
    private ProjectMembership(
            ProjectMembershipId id,
            ProjectId projectId,
            UserId userId,
            ProjectRole role) : base(id)
    {
        ProjectId = projectId;
        UserId = userId;
        Role = role;
    }
    protected ProjectMembership() { } // EF Core

    public ProjectId ProjectId { get; private set; }
    public UserId UserId { get; private set; }
    public ProjectRole Role { get; private set; }

    public static Result<ProjectMembership> Create(
           ProjectId projectId,
           UserId userId,
           ProjectRole role)
    {
        var membership = new ProjectMembership(
            ProjectMembershipId.New(),
            projectId,
            userId,
            role);

        return Result.Success(membership);
    }

    public Result ChangeRole(ProjectRole newRole)
    {
        if (!Enum.IsDefined(typeof(ProjectRole), newRole))
            return Result.Failure("Invalid project role");

        if (Role == newRole)
            return Result.Failure("User already has this role");

        Role = newRole;
        return Result.Success();
    }

    public bool HasRole(ProjectRole role) => Role == role;
    public bool IsOwner() => Role == ProjectRole.Owner;
    public bool IsModerator() => Role == ProjectRole.Moderator;
    public bool IsParticipant() => Role == ProjectRole.Member;

}
