using CSharpFunctionalExtensions;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Users;

namespace PostBinar.Domain.Projects;

public sealed class Project : Abstraction.Entity<ProjectId>
{
    private Project(
        ProjectId id,
        string name,
        string description,
        UserId ownerId,
        DateTime createdAt,
        bool isActive)
        : base(id)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
        CreatedAt = createdAt;
        IsActive = isActive;
    }
    
    // EF Core
    protected Project() { } 

    public string Name { get; private set; }
    public string Description { get; private set; }
    public UserId OwnerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation properties
    public ICollection<ProjectMembership> Members { get; set; } = new List<ProjectMembership>();
    public ICollection<UserProjectRole> UserRoles { get; set; } = new List<UserProjectRole>();
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();

}
