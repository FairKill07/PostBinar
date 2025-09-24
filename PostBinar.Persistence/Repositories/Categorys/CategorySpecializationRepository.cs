using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Categorys;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories.Categorys;

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
    public async Task<Specialization> GetByIdAsync(int categorySpecializationId)
    {
        var specialization = await _context.CategorySpecializations.FindAsync(categorySpecializationId);
        if (specialization == null)
        {
            throw new InvalidOperationException($"Specialization with ID {categorySpecializationId} not found.");
        }
        return specialization;
    }
    public async Task<List<Specialization>> GetAllAsync()
    {
        return await _context.CategorySpecializations.ToListAsync();
    }
}
