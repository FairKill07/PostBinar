using PostBinar.Domain.Categorys;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface ICategorySpecializationRepository
{
    void Add(Specialization specialization);
    void Delete(Specialization specialization);

    Task<Specialization?> GetByIdAsync(
        int categorySpecializationId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Specialization>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
