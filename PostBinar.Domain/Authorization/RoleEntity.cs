using PostBinar.Domain.Users;

namespace PostBinar.Domain.Authorization;

public sealed class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<PermissionEntity> Permissions { get; set; } = [];
}
