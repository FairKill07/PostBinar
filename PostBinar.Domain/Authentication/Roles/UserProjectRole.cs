using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Authentication.Roles;

public sealed class UserProjectRole
{
    public Guid Id { get; set; }
    public ProjectId ProjectId { get; set; }
    public UserId UserId { get; set; }
    public int RoleId { get; set; }
    public DateTimeOffset AssignedAt { get; set; }

    // Navigation properties
    public Project Project { get; set; } = null!;
    public User User { get; set; } = null!;
    public ProjectRole Role { get; set; } = null!;
}
