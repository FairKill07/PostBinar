namespace PostBinar.Domain.ProjectMemberships;

public record ProjectMembershipId (Guid Value)
{
    public static ProjectMembershipId New() => new(Guid.NewGuid());
    public static ProjectMembershipId Empty => new(Guid.Empty);
}
