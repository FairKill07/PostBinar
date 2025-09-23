using MediatR;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Projects.Queries.GetProjectById;

public sealed record GetProjectByIdQuery(ProjectId ProjectId) : IRequest<Project>;

