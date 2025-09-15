using MediatR;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Projects.Commands.CreateProject;

public sealed record CreateProjactCommand (
    string Name, 
    string Description, 
    UserId OwnerId) 
    : IRequest<ProjectId>;
