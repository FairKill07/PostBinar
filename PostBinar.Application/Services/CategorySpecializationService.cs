using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Categorys;

namespace PostBinar.Application.Services;

public sealed class CategorySpecializationService : ICategorySpecializationService
{
    private readonly ICategorySpecializationRepository _categorySpecializationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategorySpecializationService(
        ICategorySpecializationRepository categorySpecializationRepository,
        IUnitOfWork unitOfWork)
    {
        _categorySpecializationRepository = categorySpecializationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Specialization>> CreateSpecializationAsync(
        string name,
        string colorCode,
        CancellationToken cancellationToken)
    {
        var specialization = new Specialization
        {
            Name = name,
            ColorCode = colorCode
        };

        _categorySpecializationRepository.Add(specialization);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(specialization);
    }

    public async Task<Result> DeleteSpecializationAsync(
        int specializationId,
        CancellationToken cancellationToken)
    {
        var specialization = await _categorySpecializationRepository.GetByIdAsync(specializationId, cancellationToken);
        if (specialization is null)
            return Result.Failure(Error.NoData);

        _categorySpecializationRepository.Delete(specialization);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<Specialization>> GetSpecializationByIdAsync(
        int specializationId,
        CancellationToken cancellationToken)
    {
        var specialization = await _categorySpecializationRepository.GetByIdAsync(specializationId, cancellationToken);
        if (specialization is null)
            return Result.Failure<Specialization>(Error.NoData);

        return Result.Success(specialization);
    }

    public async Task<Result<IReadOnlyList<Specialization>>> GetAllSpecializationsAsync(
        CancellationToken cancellationToken)
    {
        var specializations = await _categorySpecializationRepository.GetAllAsync(cancellationToken);
        if (specializations is null || specializations.Count == 0)
            return Result.Failure<IReadOnlyList<Specialization>>(Error.NoData);

        return Result.Success(specializations);
    }
}
