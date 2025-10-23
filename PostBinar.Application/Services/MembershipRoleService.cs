using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Application.Services;

public sealed class MembershipRoleService : IMembershipRoleService
{
    private readonly IMembershipRoleRepository _membershipRoleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MembershipRoleService(IMembershipRoleRepository membershipRoleRepository, IUnitOfWork unitOfWork)
    {
        _membershipRoleRepository = membershipRoleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> AssignRoleAsync(
        ProjectMembershipId membershipId,
        Role role,
        CancellationToken cancellationToken)
    {
        var roleResult = Domain.Authorization.ProjectRole.Create(membershipId, role);
        if (roleResult.IsFailure)
            return Result.Failure(roleResult.Error);

        _membershipRoleRepository.Add(roleResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> RemoveRoleAsync(
        ProjectMembershipId membershipId,
        Role role,
        CancellationToken cancellationToken)
    {
        var projectRole = await _membershipRoleRepository.GetByIdAsync(membershipId, cancellationToken);
        if (projectRole is null)
            return Result.Failure(Error.NoData);

        _membershipRoleRepository.Delete(projectRole);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ProjectMembership>>> GetRolesForMembershipAsync(
        ProjectMembershipId projectMembershipId,
        CancellationToken cancellationToken)
    {
        var roles = await _membershipRoleRepository.GetRolesForMembershipAsync(projectMembershipId, cancellationToken);
        if (roles is null)
            return Result.Failure<IEnumerable<ProjectMembership>>(Error.NoData);

        return Result.Success(roles);
    }
}
