using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;

namespace PostBinar.Domain.FileStorages;

public sealed class FileStorage : Entity<FileStorageId>
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
        Result validationResult = ValidateParameters(projectId, objectId, fileName, storageKey, mimeType, size);
        
        if (validationResult.IsFailure)
            return Result.Failure<FileStorage>(validationResult.Error);

        var fileStorage = new FileStorage(
            FileStorageId.New(),
            projectId,
            objectType,
            objectId,
            fileName.Trim(),
            storageKey.Trim(),
            mimeType.Trim(),
            size,
            DateTimeOffset.UtcNow);

        return fileStorage;
    }


    public Result RenameFile(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure(FileStorageErrors.InvalidFileName);

        FileName = newName;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result ChangeStorageKey(string newKey, long newSize, string newMimeType)
    {
        if (string.IsNullOrWhiteSpace(newKey))
            return Result.Failure(FileStorageErrors.InvalidStorageKey);
        if (string.IsNullOrWhiteSpace(newMimeType))
            return Result.Failure(FileStorageErrors.InvalidMimeType);
        if (newSize < 0)
            return Result.Failure(FileStorageErrors.InvalidFileSize);

        StorageKey = newKey;
        Size = newSize;
        MimeType = newMimeType;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result ReassignTo(Guid newObjectId, StorageObjectType newObjectType)
    {
        if (newObjectId == Guid.Empty)
            return Result.Failure(FileStorageErrors.InvalidObjectId);

        ObjectId = newObjectId;
        ObjectType = newObjectType;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure(FileStorageErrors.AlreadyInactive);

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure(FileStorageErrors.AlreadyActive);

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    private static Result ValidateParameters(
        ProjectId projectId,
        Guid objectId,
        string fileName,
        string storageKey,
        string mimeType,
        long size)
    {
        if (projectId is null || projectId.Value == Guid.Empty)
            return Result.Failure(FileStorageErrors.InvalidProjectId);

        if (objectId == Guid.Empty)
            return Result.Failure(FileStorageErrors.InvalidObjectId);

        if (string.IsNullOrWhiteSpace(fileName))
            return Result.Failure(FileStorageErrors.InvalidFileName);

        if (string.IsNullOrWhiteSpace(storageKey))
            return Result.Failure(FileStorageErrors.InvalidStorageKey);

        if (string.IsNullOrWhiteSpace(mimeType))
            return Result.Failure(FileStorageErrors.InvalidMimeType);

        if (size < 0)
            return Result.Failure(FileStorageErrors.InvalidFileSize);

        return Result.Success();
    }
}
