using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IFileStorageRepository
{
    void Add(FileStorage fileStorage);
    void Delete(FileStorage fileStorage);
    Task<FileStorage?> GetByIdAsync(FileStorageId fileStorageId);
    Task<List<FileStorage>> GetByObjectAsync(ProjectId objectId, StorageObjectType storageObjectType);
}
