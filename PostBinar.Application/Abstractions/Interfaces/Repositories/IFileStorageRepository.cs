using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IFileStorageRepository
{
    void Add(FileStorage fileStorage);
    void Delete(FileStorage fileStorage);

    Task<FileStorage?> GetByIdAsync(
        FileStorageId fileStorageId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FileStorage>> GetByObjectAsync(
        Guid objectId, 
        StorageObjectType storageObjectType,
        CancellationToken cancellationToken = default);
}
