using PostBinar.Domain.Authentication.Permissions;

namespace PostBinar.Domain.Authentication.Roles;

public sealed class RolePermission
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public ProjectRole Role { get; set; } = null!;
    public int PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}
