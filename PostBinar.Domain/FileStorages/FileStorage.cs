using CSharpFunctionalExtensions;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;

namespace PostBinar.Domain.FileStorages;

public sealed class FileStorage : Abstraction.Entity<FileStorageId>
{
    private FileStorage(
        FileStorageId id,
        ProjectId projectId,
        StorageObjectType objectType,
        Guid objectId,
        string fileName,
        string storageKey,
        string mimeType,
        long size,
        DateTimeOffset createdAt)
        : base(id)
    {
        ProjectId = projectId;
        ObjectType = objectType;
        ObjectId = objectId;
        FileName = fileName;
        StorageKey = storageKey;
        MimeType = mimeType;
        Size = size;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
        IsActive = true;
    }

    // EF Core
    protected FileStorage() { }

    public ProjectId ProjectId { get; private set; }
    public StorageObjectType ObjectType { get; private set; }
    public Guid ObjectId { get; private set; }
    public string FileName { get; private set; } = null!;
    public string StorageKey { get; private set; } = null!;
    public string MimeType { get; private set; } = null!;
    public long Size { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }


    public static Result<FileStorage> Create(
        ProjectId projectId,
        StorageObjectType objectType,
        Guid objectId,
        string fileName,
        string storageKey,
        string mimeType,
        long size)
    {
        if (projectId == null || projectId.Value == Guid.Empty)
            return Result.Failure<FileStorage>("Project ID is required");
        if (objectId == Guid.Empty)
            return Result.Failure<FileStorage>("Object ID is required");
        if (string.IsNullOrWhiteSpace(fileName))
            return Result.Failure<FileStorage>("File name is required");
        if (string.IsNullOrWhiteSpace(storageKey))
            return Result.Failure<FileStorage>("Storage key is required");
        if (string.IsNullOrWhiteSpace(mimeType))
            return Result.Failure<FileStorage>("Mime type is required");
        if (size < 0)
            return Result.Failure<FileStorage>("File size cannot be negative");

        var fileStorage = new FileStorage(
            FileStorageId.New(),
            projectId,
            objectType,
            objectId,
            fileName,
            storageKey,
            mimeType,
            size,
            DateTimeOffset.UtcNow);

        return Result.Success(fileStorage);
    }

    public Result RenameFile(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure("File name cannot be empty");

        FileName = newName;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result ChangeStorageKey(string newKey, long newSize, string newMimeType)
    {
        if (string.IsNullOrWhiteSpace(newKey))
            return Result.Failure("Storage key is required");
        if (string.IsNullOrWhiteSpace(newMimeType))
            return Result.Failure("Mime type is required");
        if (newSize < 0)
            return Result.Failure("File size cannot be negative");

        StorageKey = newKey;
        Size = newSize;
        MimeType = newMimeType;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result ReassignTo(Guid newObjectId, StorageObjectType newObjectType)
    {
        if (newObjectId == Guid.Empty)
            return Result.Failure("Object ID is required");

        ObjectId = newObjectId;
        ObjectType = newObjectType;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure("File is already inactive");

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure("File is already active");

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }
}
