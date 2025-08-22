using CSharpFunctionalExtensions;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.ProjectsMemberships;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Projects;

public sealed class Project : Abstraction.Entity<ProjectId>
{
    private readonly List<ProjectMembership> _memberships = new();

    private Project(
        ProjectId id,
        string name,
        string description,
        UserId ownerId,
        DateTime createdAt,
        DateTime updatedAt)
        : base(id)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    protected Project() { } // EF Core

    public string Name { get; private set; }
    public string Description { get; private set; }
    public UserId OwnerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyCollection<ProjectMembership> Memberships => _memberships.AsReadOnly();

    public static Result<Project> Create(
        string name,
        string description,
        UserId ownerId,
        DateTime createdAt,
        DateTime updatedAt)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Project>("Project name cannot be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Project>("Project description cannot be empty");

        var project = new Project(
            ProjectId.New(),
            name,
            description,
            ownerId,
            createdAt,
            updatedAt);

        return Result.Success(project);
    }

    public Result Update(string name, string description, DateTime updatedAt)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Project name cannot be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure("Project description cannot be empty");

        Name = name;
        Description = description;
        UpdatedAt = updatedAt;
        return Result.Success();
    }

    public Result AddMembership(ProjectMembership membership)
    {
        if (membership is null)
            return Result.Failure("Membership cannot be null");

        if (_memberships.Any(m => m.UserId == membership.UserId))
            return Result.Failure("User is already a member of this project");

        _memberships.Add(membership);
        return Result.Success();
    }

    public Result RemoveMembership(UserId userId, UserId requesterId)
    {
        var requester = FindMembership(requesterId);
        if (requester is null)
            return Result.Failure("Requester is not a member of this project");

        var target = FindMembership(userId);
        if (target is null)
            return Result.Failure("User is not a member of this project");

        if (target.IsOwner())
            return Result.Failure("Project owner cannot be removed");

        if (!requester.IsOwner() && !requester.IsModerator())
            return Result.Failure("Only owners and moderators can remove members");

        if (requester.IsModerator() && target.IsModerator())
            return Result.Failure("Moderators cannot remove other moderators");

        _memberships.Remove(target);
        return Result.Success();
    }

    public Result PromoteToModerator(UserId userId, UserId requesterId)
    {
        var requester = FindMembership(requesterId);
        if (requester?.IsOwner() != true)
            return Result.Failure("Only owner can promote users to moderator");

        var target = FindMembership(userId);
        if (target is null)
            return Result.Failure("User is not a member of this project");

        return target.ChangeRole(ProjectRole.Moderator);
    }

    public Result DemoteToMemeber(UserId userId, UserId requesterId)
    {
        var requester = FindMembership(requesterId);
        if (requester?.IsOwner() != true)
            return Result.Failure("Only owner can demote moderators");

        var target = FindMembership(userId);
        if (target is null)
            return Result.Failure("User is not a member of this project");

        if (target.IsOwner())
            return Result.Failure("Cannot demote project owner");

        return target.ChangeRole(ProjectRole.Member);
    }

    private ProjectMembership? FindMembership(UserId userId) =>
        _memberships.FirstOrDefault(m => m.UserId == userId);
}
