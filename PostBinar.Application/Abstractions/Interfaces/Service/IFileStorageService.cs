using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IFileStorageService
{
    Task<FileStorage> UploadFileAsync(
        ProjectId projectId,
        Guid objectId,
        Stream fileStream,
        StorageObjectType storageObjectType,
        string fileName,
        string? mimeType,
        long size,
        CancellationToken cancellationToken);

    Task<string> GetFileDownloadUrlAsync(
        FileStorageId fileStorageId,
        CancellationToken cancellationToken);

    Task<bool> DeleteFileAsync(
        FileStorageId fileStorageId,
        CancellationToken cancellationToken);

    Task<List<FileStorage>> GetFilesByObjectAsync(
        ProjectId objectId,
        StorageObjectType storageObjectType);
}
