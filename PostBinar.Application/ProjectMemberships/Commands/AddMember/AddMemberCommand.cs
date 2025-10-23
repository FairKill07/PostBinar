using MediatR;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.ProjectMemberships.Commands.AddMember;

public sealed record AddMemberCommand(
    ProjectId ProjectId,
    UserId UserId,
    string Role,
    CancellationToken CancellationToken
    ) : IRequest<Result>;
