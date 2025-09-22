using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(ProjectId projectId);
    void Add(Project project);
    void Delete(Project project);
    void Update(Project project);
}
