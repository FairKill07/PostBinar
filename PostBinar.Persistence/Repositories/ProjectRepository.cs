using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Projects;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class ProjectRepository : Repository<Project, ProjectId>, IProjectRepository
{
    public ProjectRepository(PostBinarDbContext context) : base(context) { }

    public void Update(Project project)
    {
        _context.Projects.Update(project);
    }
}
