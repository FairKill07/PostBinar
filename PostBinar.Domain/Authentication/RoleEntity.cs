using PostBinar.Domain.Users;

namespace PostBinar.Domain.Authentication;

public sealed class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = [];
    public ICollection<PermissionEntity> Permissions { get; set; } = [];
}
