namespace PostBinar.Domain.FileStorages;

public record FileStorageId(Guid Value)
{
    public static FileStorageId New() => new(Guid.NewGuid());
    public static FileStorageId Empty => new(Guid.Empty);
}
