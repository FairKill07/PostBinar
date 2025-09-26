using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Abstractions.Interfaces.IFileStorage;

public interface IFileHelper
{
    string GenerateObjectKey(
        ProjectId projectId, 
        StorageObjectType storageObjectType, 
        Guid objectId, 
        string fileName);

    FileStorage CreateStoredFile(
        ProjectId projectId,
        StorageObjectType storageObjectType,
        Guid objectId,
        string fileName,
        string storageKey,
        long size,
        string? mimeType);
}
