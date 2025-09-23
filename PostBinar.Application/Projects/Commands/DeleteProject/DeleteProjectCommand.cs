using MediatR;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Projects.Commands.DeleteProject;

public sealed record DeleteProjectCommand(ProjectId ProjectId) : IRequest<Unit>;
