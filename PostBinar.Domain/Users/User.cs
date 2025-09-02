using CSharpFunctionalExtensions;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Domain.Users;

public sealed class User : Abstraction.Entity<UserId>
{
    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        int specializationId)
        : base(id)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.PasswordHash = passwordHash;
        this.SpecializationId = specializationId;
    }
    protected User() { } //EF core

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int SpecializationId { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? TgChatId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Navigation properties
    public Specialization? Specialization { get; set; }
    public ICollection<ProjectMembership> ProjectMemberships { get; set; } = new List<ProjectMembership>();
    public ICollection<UserProjectRole> ProjectRoles { get; set; } = new List<UserProjectRole>();

    
    public static Result<User> Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        int specializationId)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<User>("First name is required");
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<User>("Last name is required");
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<User>("Email is required");
       
        var user = new User(
            UserId.New(),
            firstName,
            lastName,
            email,
            passwordHash,
            specializationId);

        return Result.Success(user);
    }
}
