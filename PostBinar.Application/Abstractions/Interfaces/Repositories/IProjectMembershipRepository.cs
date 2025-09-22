using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IProjectMembershipRepository
{
    Task<ProjectMembership?> GetMembershipAsync(ProjectId projectId, UserId userId); 
    Task<IEnumerable<ProjectMembership>> GetAllForProjectAsync(ProjectId projectId);  
    void Add(ProjectMembership membership);
    void Delete(ProjectMembership membership);

}
