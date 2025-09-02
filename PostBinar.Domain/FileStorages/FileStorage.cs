using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;

namespace PostBinar.Domain.FileStorages;

public sealed class FileStorage : Abstraction.Entity<FileStorageId>
{
    public ProjectId ProjectId { get; set; }
    public StorageObjectType ObjectType { get; set; }
    public Guid ObjectId { get; set; }
    public string FileName { get; set; } = null!;
    public string StorageKey { get; set; } = null!;
    public string MimeType { get; set; } = null!;
    public long Size { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    // Navigation properties
    public Project Project { get; set; } = null!;
}
