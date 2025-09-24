using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Domain.Categorys;

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

    public async Task<Specialization> CreateSpecializationAsync(string name, string colorCode)
    {
        var specialization = new Specialization
        {
            Name = name,
            ColorCode = colorCode
        };

        _categorySpecializationRepository.Add(specialization);
        await _unitOfWork.SaveChangesAsync(); 

        return specialization;
    }

    public async Task DeleteSpecializationAsync(int specializationId)
    {
        var specialization = await _categorySpecializationRepository.GetByIdAsync(specializationId);
        if (specialization != null)
        {
            _categorySpecializationRepository.Delete(specialization);
            await _unitOfWork.SaveChangesAsync(); 
        }
    }

    public Task<List<Specialization>> GetAllSpecializationsAsync()
    {
        return _categorySpecializationRepository.GetAllAsync();
    }

    public async Task<Specialization> GetSpecializationByIdAsync(int specializationId)
    {
        var specialization = await _categorySpecializationRepository.GetByIdAsync(specializationId);
        if (specialization == null)
        {
            throw new Exception("Specialization not found");
        }
        return specialization;
    }
}
