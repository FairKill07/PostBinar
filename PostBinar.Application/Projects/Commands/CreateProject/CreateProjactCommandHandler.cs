using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Projects.Commands.CreateProject;

public sealed class CreateProjactCommandHandler : IRequestHandler<CreateProjactCommand, ProjectId>
{
    private readonly IProjectService _projectService;
    public CreateProjactCommandHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }
    public async Task<ProjectId> Handle(CreateProjactCommand request, CancellationToken cancellationToken)
    {
        var projectId = await _projectService.CreateProjectAsync(
            request.Name,
            request.Description,
            request.OwnerId);

        return projectId;
    }
}
