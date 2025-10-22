using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class ProjectMembershipRepository
    : Repository<ProjectMembership, ProjectMembershipId>, IProjectMembershipRepository
{
    public ProjectMembershipRepository(PostBinarDbContext context) : base(context) { }

    public async Task<IReadOnlyList<ProjectMembership>> GetAllForProjectAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ProjectMembership> query = _context.ProjectMemberships
            .Where(m => m.ProjectId == projectId);

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProjectMembership>> GetAllForUserAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ProjectMembership> query = _context.ProjectMemberships
            .Where(m => m.UserId == userId)
            .Include(m => m.Project);

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ProjectMembership?> GetMembershipAsync(
        ProjectId projectId,
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProjectMemberships
            .FirstOrDefaultAsync(m =>
                m.ProjectId == projectId &&
                m.UserId == userId,
                cancellationToken);
    }
}
