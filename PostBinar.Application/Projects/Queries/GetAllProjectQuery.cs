using MediatR;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Projects.Queries;

public record GetAllProjectQuery(UserId UserId) : IRequest<ProjectListVm>;
