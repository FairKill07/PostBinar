using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Categorys;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Domain.Users;

public sealed class User : Entity<UserId>
{
    private readonly List<ProjectMembership> _projectMemberships = [];

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
    public Specialization Specialization { get; private set; } = null!;
    public string? ProfilePhoto { get; private set; }
    public string? TgChatId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public IReadOnlyCollection<ProjectMembership> ProjectMemberships => _projectMemberships;

    public static Result<User> Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        int specializationId)
    {
        Result validationResult = ValidateParameters(firstName, lastName, email, passwordHash);
        
        if (validationResult.IsFailure)
        {
            return Result.Failure<User>(validationResult.Error);
        }

        var user = new User(
            UserId.New(),
            firstName,
            lastName,
            email,
            passwordHash,
            specializationId,
            DateTimeOffset.UtcNow);

        return user;
    }

    public Result Update(
        string firstName,
        string lastName,
        string passwordHash,
        int specializationId)
    {

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

    private static Result ValidateParameters(
        string firstName,
        string lastName,
        string email,
        string passwordHash)
    {
        {
            if (
                string.IsNullOrWhiteSpace(firstName)
                || string.IsNullOrWhiteSpace(lastName)
                || string.IsNullOrWhiteSpace(email)
                || string.IsNullOrWhiteSpace(passwordHash)
            )
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            return Result.Success();
        }

    }
}
