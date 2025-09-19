using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Projects.Commands.UpdateProject
{
    public sealed class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Project>
    {
        private readonly IProjectService _projectService;
        public UpdateProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<Project> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectService.UpdateProjectAsync(
                ownerId: request.OwnerId,
                projectId: request.ProjectId,
                name: request.Name,
                description: request.Description
            );
            return project;
        }
    }
}
