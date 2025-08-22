namespace PostBinar.Domain.Projects;

public record ProjectId(Guid Value)
{
    public static ProjectId New() => new(Guid.NewGuid());
    public static ProjectId Empty => new(Guid.Empty);
}
