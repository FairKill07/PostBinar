using PostBinar.Application.Abstractions.Interfaces.IFileStorage;
using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Infrastructure.MinIO;

public class MinioFileHelper : IFileHelper
{
    public string GenerateObjectKey(ProjectId projectId, StorageObjectType storageObjectType, Guid objectId, string fileName)
    {
        if (projectId == null)
            throw new ArgumentNullException(nameof(projectId));
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be empty.", nameof(fileName));

        var safeName = string.Join("_",
            Path.GetFileName(fileName)
                .Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

        return $"{projectId.Value}/{storageObjectType}/{objectId}/{safeName}";
    }

    public FileStorage CreateStoredFile(
        ProjectId projectId,
        StorageObjectType storageObjectType,
        Guid objectId,
        string fileName,
        string storageKey,
        long size,
        string? mimeType)
    {
        var result = FileStorage.Create(
            projectId,
            storageObjectType,
            objectId,
            fileName,
            storageKey,
            mimeType ?? "application/octet-stream",
            size);

        if (result.IsFailure)
            throw new InvalidOperationException($"Error creating FileStorage: {result.Error}");

        return result.Value;
    }
}
