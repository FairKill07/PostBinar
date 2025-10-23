using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Authorization;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.ProjectMemberships;

public sealed class ProjectMembership : Entity<ProjectMembershipId>
{
    private readonly List<ProjectRole> _roles = [];
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
    public Project Project { get; private set; } = null!;
    public UserId UserId { get; private set; }
    public User User { get; private  set; } = null!;
    public DateTimeOffset JoinedAt { get; private set; }
    public IReadOnlyCollection<ProjectRole> Roles => _roles;


    public static Result<ProjectMembership> Create(ProjectId projectId, UserId userId)
    {
        if (projectId == null || projectId.Value == Guid.Empty)
            return Result.Failure<ProjectMembership>(Error.NullValue);
        if (userId == null || userId.Value == Guid.Empty)
            return Result.Failure<ProjectMembership>(Error.NullValue);

        var membership = new ProjectMembership(
            ProjectMembershipId.New(),
            projectId,
            userId,
            DateTimeOffset.UtcNow);

        return membership;
    }
}
