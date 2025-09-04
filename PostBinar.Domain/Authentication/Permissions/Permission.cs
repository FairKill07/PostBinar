using PostBinar.Domain.Authentication.Roles;

namespace PostBinar.Domain.Authentication.Permissions;

public sealed class Permission
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;        // Например: "TASK_CREATE"
    public string Description { get; set; } = null!;

    // Navigation properties
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
