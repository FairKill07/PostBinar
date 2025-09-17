using MediatR;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand (
    string Name, 
    string Description, 
    UserId OwnerId) 
    : IRequest<ProjectId>;
