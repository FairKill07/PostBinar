using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Abstractions.Interfaces.IFileStorage;

public interface IFileStorage
{
    Task EnsureBucketExistsAsync(CancellationToken cancellationToken);

    Task<FileStorage> UploadFileAsync(
            ProjectId projectId,
            Guid objectId,
            string storageKey,
            Stream fileStream,
            StorageObjectType file,
            string fileName,
            string? mimeType,
            long size,
            CancellationToken cancellationToken);

    Task<string> GetFileDownloadUrlAsync(
        string storageKey, 
        CancellationToken cancellationToken);

    Task<bool> DeleteFileAsync(
        string storageKey,
        CancellationToken cancellationToken);
}
