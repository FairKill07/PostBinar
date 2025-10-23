using PostBinar.Application.Common.Models.FileStorage;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IFileStorageService
{
    Task<Result<FileStorage>> UploadFileAsync(
        ProjectId projectId,
        Guid objectId,
        Stream fileStream,
        StorageObjectType storageObjectType,
        string fileName,
        string? mimeType,
        long size,
        CancellationToken cancellationToken);

    Task<Result<FileUrlResponse>> GetFileDownloadUrlAsync(
        FileStorageId fileStorageId,
        CancellationToken cancellationToken);

    Task<Result> DeleteFileAsync(
        FileStorageId fileStorageId,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<FileStorage>>> GetFilesByObjectAsync(
        Guid objectId,
        StorageObjectType storageObjectType,
        CancellationToken cancellationToken);
}
