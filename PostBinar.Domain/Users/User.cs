using CSharpFunctionalExtensions;
using PostBinar.Domain.Authentication.Roles;
using PostBinar.Domain.Categorys;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Domain.Users;

public sealed class User : Abstraction.Entity<UserId>
{
    private readonly HashSet<ProjectMembership> _projectMemberships = [];
    private readonly HashSet<UserProjectRole> _projectRoles = [];

    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        int specializationId,
        DateTimeOffset createdAt)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        SpecializationId = specializationId;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    protected User() { } // EF Core

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public int SpecializationId { get; private set; }
    public string? ProfilePhoto { get; private set; }
    public string? TgChatId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    // Навигационные свойства
    public Specialization? Specialization { get; private set; }
    public IReadOnlyCollection<ProjectMembership> ProjectMemberships => _projectMemberships;
    public IReadOnlyCollection<UserProjectRole> ProjectRoles => _projectRoles;

    public static Result<User> Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        int specializationId,
        DateTimeOffset createdAt)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<User>("First name is required");
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<User>("Last name is required");
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<User>("Email is required");
        if (string.IsNullOrWhiteSpace(passwordHash))
            return Result.Failure<User>("Password hash is required");

        var user = new User(
            UserId.New(),
            firstName,
            lastName,
            email,
            passwordHash,
            specializationId,
            createdAt);

        return Result.Success(user);
    }

    public Result Update(
        string firstName,
        string lastName,
        string passwordHash,
        int specializationId)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure("First name is required");
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure("Last name is required");
        if (string.IsNullOrWhiteSpace(passwordHash))
            return Result.Failure("Password hash is required");

        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        SpecializationId = specializationId;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public void UpdateProfilePhoto(string? profilePhoto)
    {
        ProfilePhoto = profilePhoto;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateTgChatId(string? tgChatId)
    {
        TgChatId = tgChatId;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
