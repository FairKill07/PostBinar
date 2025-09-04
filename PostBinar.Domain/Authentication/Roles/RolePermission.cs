using PostBinar.Domain.Authentication.Permissions;

namespace PostBinar.Domain.Authentication.Roles;

public sealed class RolePermission
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

}
