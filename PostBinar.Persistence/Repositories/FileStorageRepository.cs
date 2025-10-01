using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class FileStorageRepository : Repository<FileStorage, FileStorageId>, IFileStorageRepository
{
    public FileStorageRepository(PostBinarDbContext context) : base(context)
    {
    }
    public Task<List<FileStorage>> GetByObjectAsync(Guid objectId, StorageObjectType storageObjectType)
    {
        return _context.FileStorages
            .Where(fs => fs.ObjectId == objectId && fs.ObjectType == storageObjectType && fs.IsActive)
            .ToListAsync();
    }
}
