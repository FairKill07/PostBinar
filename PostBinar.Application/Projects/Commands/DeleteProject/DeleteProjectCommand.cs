using MediatR;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Projects.Commands.DeleteProject;

public sealed record DeleteProjectCommand(
    ProjectId ProjectId,
    CancellationToken CancellationToken) : IRequest<Result>;
