namespace PostBinar.Domain.Users;

public record UserId (Guid Value)
{
    public static UserId New() => new(Guid.NewGuid());
    public static UserId Empty => new(Guid.Empty);
}
