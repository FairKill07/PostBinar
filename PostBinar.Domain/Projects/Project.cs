using CSharpFunctionalExtensions;
using PostBinar.Domain.Authentication.Roles;
using PostBinar.Domain.Notes;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Projects;

public sealed class Project : Abstraction.Entity<ProjectId>
{
    private readonly HashSet<ProjectMembership> _members = [];
    private readonly HashSet<UserProjectRole> _userRoles = [];
    private readonly HashSet<TaskItem> _tasks = [];
    private readonly HashSet<Note> _notes = [];

    private Project(
        ProjectId id,
        string name,
        string description,
        UserId ownerId,
        DateTimeOffset createdAt,
        bool isActive)
        : base(id)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
        IsActive = isActive;
    }

    // EF Core
    protected Project() { }

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public UserId OwnerId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation
    public IReadOnlyCollection<ProjectMembership> Members => _members;
    public IReadOnlyCollection<UserProjectRole> UserRoles => _userRoles;
    public IReadOnlyCollection<TaskItem> Tasks => _tasks;
    public IReadOnlyCollection<Note> Notes => _notes;

    public static Result<Project> Create(
        string name,
        string description,
        UserId ownerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Project>("Project name is required");
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Project>("Project description is required");
        if (ownerId == null || ownerId.Value == Guid.Empty)
            return Result.Failure<Project>("Owner ID is required");

        var now = DateTimeOffset.UtcNow;
        var project = new Project(
            ProjectId.New(),
            name,
            description,
            ownerId,
            now,
            true);

        return Result.Success(project);
    }

    public Result AddMember(UserId userId, int roleId)
    {
        if (userId == null || userId.Value == Guid.Empty)
            return Result.Failure("User ID is required");
        if (OwnerId == userId)
            return Result.Failure("Owner is already a member");

        var membership = ProjectMembership.Create(Id, userId);
        if (membership.IsFailure)
            return Result.Failure(membership.Error);

        _members.Add(membership.Value);
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }
    public Result Update(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Project name is required");
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure("Project description is required");

        Name = name;
        Description = description;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result RemoveMember(UserId userId)
    {
        if (userId == null || userId.Value == Guid.Empty)
            return Result.Failure("User ID is required");

        if (OwnerId == userId)
            return Result.Failure("Cannot remove project owner");

        var membership = _members.FirstOrDefault(m => m.UserId == userId);
        if (membership == null)
            return Result.Failure("User is not a member of this project");

        var userRole = _userRoles.FirstOrDefault(ur => ur.UserId == userId);

        _members.Remove(membership);
        if (userRole != null)
        {
            _userRoles.Remove(userRole);
        }

        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public bool IsMember(UserId userId)
    {
        return _members.Any(m => m.UserId == userId);
    }

    public bool IsOwner(UserId userId)
    {
        return OwnerId == userId;
    }

    public IEnumerable<UserId> GetMemberIds()
    {
        return _members.Select(m => m.UserId);
    }

    public Result<ProjectRole?> GetUserRole(UserId userId)
    {
        if (userId == null || userId.Value == Guid.Empty)
            return Result.Failure<ProjectRole?>("User ID is required");

        if (!IsMember(userId))
            return Result.Failure<ProjectRole?>("User is not a member of this project");

        var userRole = _userRoles.FirstOrDefault(ur => ur.UserId == userId);
        return Result.Success(userRole?.Role);
    }
    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure("Project is already inactive");

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure("Project is already active");

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }
}
