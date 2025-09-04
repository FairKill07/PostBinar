namespace PostBinar.Domain.Authentication.Roles;

public sealed class ProjectRole
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;        
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<UserProjectRole> UserRoles { get; set; } = new List<UserProjectRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
