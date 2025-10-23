using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Categorys;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface ICategorySpecializationService
{
    Task<Result<Specialization>> CreateSpecializationAsync(
        string name,
        string colorCode,
        CancellationToken cancellationToken);

    Task<Result> DeleteSpecializationAsync(
        int specializationId,
        CancellationToken cancellationToken);

    Task<Result<Specialization>> GetSpecializationByIdAsync(
        int specializationId,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Specialization>>> GetAllSpecializationsAsync(
        CancellationToken cancellationToken);
}
