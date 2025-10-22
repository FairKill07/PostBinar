using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IProjectRepository
{
    void Add(Project project);
    void Delete(Project project);

    Task<Project?> GetByIdAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default);
}
