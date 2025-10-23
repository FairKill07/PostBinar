using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;

namespace PostBinar.Application.ProjectMemberships.Commands.AddMember;

public sealed class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, Result>
{
    private readonly IProjectMembershipService _projectMembershipService;
    private readonly IMembershipRoleService _membershipRoleService;

    public AddMemberCommandHandler(
        IProjectMembershipService projectMembershipService,
        IMembershipRoleService membershipRoleService)
    {
        _projectMembershipService = projectMembershipService;
        _membershipRoleService = membershipRoleService;
    }

    public async Task<Result> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<Role>(request.Role, ignoreCase: true, out var role))
            return Result.Failure(new Error("Role.Invalid", $"Invalid role: {request.Role}"));

        var memberResult = await _projectMembershipService.AddMemberAsync(
            request.ProjectId,
            request.UserId,
            cancellationToken);

        if (memberResult.IsFailure)
            return Result.Failure(memberResult.Error);

        var roleResult = await _membershipRoleService.AssignRoleAsync(
            memberResult.Value.Id,
            role,
            cancellationToken);

        if (roleResult.IsFailure)
            return Result.Failure(roleResult.Error);

        return Result.Success();
    }
}
