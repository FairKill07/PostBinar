using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Authorization;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class MembershipRoleRepository : IMembershipRoleRepository
{
    private readonly PostBinarDbContext _context;
    public MembershipRoleRepository(PostBinarDbContext context) 
    {
        _context = context;
    }
    public void Add(ProjectRole projectRole)
    {
        _context.ProjectRoles.Add(projectRole);
    }

    public void Delete(ProjectRole projectRole)
    {
        _context.ProjectRoles.Remove(projectRole);
    }

    public async Task<ProjectRole> GetByIdAsync(ProjectMembershipId projectMembershipId)
    {
        var projectRole = await _context.ProjectRoles.FindAsync(projectMembershipId);
        if (projectRole == null)
        {
            throw new InvalidOperationException($"ProjectRole with ID {projectMembershipId.Value} not found.");
        }
        return projectRole;
    }

    public async Task<IEnumerable<ProjectMembership>> GetRolesForMembershipAsync(ProjectMembershipId projectMembershipId)
    {
        return await _context.ProjectMemberships
                     .Where(pm => pm.Id == projectMembershipId)
                     .ToListAsync();
    }
}
