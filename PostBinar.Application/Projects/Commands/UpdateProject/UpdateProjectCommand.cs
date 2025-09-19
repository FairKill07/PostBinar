using MediatR;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Projects.Commands.UpdateProject;

public sealed record UpdateProjectCommand(
    UserId OwnerId,
    ProjectId ProjectId,
    string Name,
    string Description) : IRequest<Project>;
