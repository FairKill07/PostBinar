using PostBinar.Domain.Categorys;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface ICategorySpecializationService
{
    Task<Specialization> CreateSpecializationAsync(string name , string colorCode);
    Task DeleteSpecializationAsync(int specializationId);
    Task<Specialization> GetSpecializationByIdAsync(int specializationId);
    Task<List<Specialization>> GetAllSpecializationsAsync();
}
