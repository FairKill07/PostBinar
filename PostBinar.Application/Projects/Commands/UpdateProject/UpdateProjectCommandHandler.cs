using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Projects.Commands.UpdateProject;

public sealed class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result>
{
    private readonly IProjectService _projectService;

    public UpdateProjectCommandHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var result = await _projectService.UpdateProjectAsync(
            ownerId: request.OwnerId,
            projectId: request.ProjectId,
            name: request.Name,
            description: request.Description,
            cancellationToken: cancellationToken);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        return Result.Success();
    }
}
