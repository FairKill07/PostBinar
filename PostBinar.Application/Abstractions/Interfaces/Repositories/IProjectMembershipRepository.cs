using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IProjectMembershipRepository
{
    Task<ProjectMembership?> GetMembershipAsync(ProjectId projectId, UserId userId); 
    Task<List<ProjectMembership>> GetAllForProjectAsync(ProjectId projectId);  
    Task<List<ProjectMembership>> GetAllForUserAsync(UserId userId);
    void Add(ProjectMembership membership);
    void Delete(ProjectMembership membership);

}
