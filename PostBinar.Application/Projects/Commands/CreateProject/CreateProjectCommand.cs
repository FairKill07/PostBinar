using MediatR;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand (
    string Name, 
    string Description, 
    UserId OwnerId,
    CancellationToken CancellationToken) 
    : IRequest<Result<ProjectId>>;
