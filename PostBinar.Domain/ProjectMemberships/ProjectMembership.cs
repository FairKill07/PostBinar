using CSharpFunctionalExtensions;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.ProjectMemberships;

public sealed class ProjectMembership : Abstraction.Entity<ProjectMembershipId>
{

    private ProjectMembership(
        ProjectMembershipId id,
        ProjectId projectId,
        UserId userId,
        DateTimeOffset joinedAt)
        : base(id)
    {
        ProjectId = projectId;
        UserId = userId;
        JoinedAt = joinedAt;
    }

    // EF Core
    protected ProjectMembership() { }

    public ProjectId ProjectId { get; private set; }
    public UserId UserId { get; private set; }
    public DateTimeOffset JoinedAt { get; private set; }

    // Навигации
    public Project Project { get; private set; } = null!;
    public User User { get; private set; } = null!;

    public static Result<ProjectMembership> Create(ProjectId projectId, UserId userId)
    {
        if (projectId == null || projectId.Value == Guid.Empty)
            return Result.Failure<ProjectMembership>("Project ID is required");
        if (userId == null || userId.Value == Guid.Empty)
            return Result.Failure<ProjectMembership>("User ID is required");

        var membership = new ProjectMembership(
            ProjectMembershipId.New(),
            projectId,
            userId,
            DateTimeOffset.UtcNow);

        return Result.Success(membership);
    }
}
