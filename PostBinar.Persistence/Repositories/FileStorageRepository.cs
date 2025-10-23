using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class FileStorageRepository : Repository<FileStorage, FileStorageId>, IFileStorageRepository
{
    public FileStorageRepository(PostBinarDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<FileStorage>> GetByObjectAsync(
        Guid objectId,
        StorageObjectType storageObjectType,
        CancellationToken cancellationToken = default)
    {
        IQueryable<FileStorage> query = _context.FileStorages
            .Where(fs => fs.ObjectId == objectId &&
                         fs.ObjectType == storageObjectType &&
                         fs.IsActive);

        var list = await query.ToListAsync(cancellationToken);
        return list;
    }
}
