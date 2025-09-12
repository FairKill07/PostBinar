using PostBinar.Domain.Abstraction;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal class Repository<TEntity, TEntityId>
where TEntity : Entity<TEntityId>
where TEntityId : class
{
    protected readonly PostBinarDbContext _context;
    public Repository(PostBinarDbContext context) => _context = context;

    public async Task<TEntity?> GetByIdAsync(TEntityId id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public virtual void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
}
