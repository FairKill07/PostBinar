using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class ProjectRepository : Repository<Project, ProjectId>, IProjectRepository
{
    public ProjectRepository(PostBinarDbContext context) : base(context) { }

    public void Update(Project project)
    {
        _context.Projects.Update(project);
    }

    public async Task<IReadOnlyList<Project>> GetActiveProjectsByUserIdAsync(UserId userId, CancellationToken ct = default)
    {
        return await _context.Projects
            .Include(p => p.ProjectMemberships)
            .Where(p => p.OwnerId == userId || p.ProjectMemberships.Any(m => m.UserId == userId) && p.IsActive)
            .ToListAsync(ct);
    }
}
