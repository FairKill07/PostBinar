using PostBinar.Domain.ProjectMemberships;
using PostBinar.Persistence.DbContects;
using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Repositories;

internal sealed class ProjectMembershipRepository : Repository<ProjectMembership, ProjectMembershipId> , IProjectMembershipRepository
{
    public ProjectMembershipRepository(PostBinarDbContext context) : base(context) { }

    public async Task<IEnumerable<ProjectMembership>> GetAllByProjectIdAsync(ProjectId projectId)
    {
        return await _context.ProjectMemberships
            .Where(m => m.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<ProjectMembership?> GetByUserAndProjectAsync(ProjectId projectId, UserId userId)
    {
        return await _context.ProjectMemberships
            .FirstOrDefaultAsync(m => m.ProjectId == projectId && m.UserId == userId);
    }
}
