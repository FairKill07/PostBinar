using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Categorys;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class CategorySpecializationRepository : ICategorySpecializationRepository
{
    private readonly PostBinarDbContext _context;

    public CategorySpecializationRepository(PostBinarDbContext context)
    {
        _context = context;
    }

    public void Add(Specialization specialization)
    {
        _context.CategorySpecializations.Add(specialization);
    }

    public void Delete(Specialization specialization)
    {
        _context.CategorySpecializations.Remove(specialization);
    }

    public async Task<Specialization> GetByIdAsync(
        int categorySpecializationId,
        CancellationToken cancellationToken = default)
    {
        var specialization = await _context.CategorySpecializations
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == categorySpecializationId, cancellationToken);

        if (specialization is null)
            throw new InvalidOperationException($"Specialization with ID {categorySpecializationId} not found.");

        return specialization;
    }

    public async Task<IReadOnlyList<Specialization>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        IQueryable<Specialization> query = _context.CategorySpecializations;

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
