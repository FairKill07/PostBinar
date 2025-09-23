using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.Projects.Commands.DeleteProject;

public sealed class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
{
    private readonly IProjectService _projectService;

    public DeleteProjectCommandHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        await _projectService.DeleteProject(request.ProjectId);
        return Unit.Value;
    }
}
