namespace PostBinar.Infrastructure.Authorization;

public class AuthorizationOptions
{
    public RolePermission[] RolePermission { get; set; } = [];
}

public class RolePermission
{
    public string Role { get; set; } = string.Empty;
    public string [] Permissions { get; set; } = [];
}
