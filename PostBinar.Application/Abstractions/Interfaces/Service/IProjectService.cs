using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IProjectService
{
    Task<ProjectId> CreateProjectAsync(string Name, string Description, UserId OwnerId);

}
