using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Services;

public sealed class ProjectMembershipService : IProjectMembershipService
{
    private readonly IProjectMembershipRepository _membershipRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectMembershipService(
        IProjectMembershipRepository membershipRepository,
        IUnitOfWork unitOfWork)
    {
        _membershipRepository = membershipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProjectMembership>> AddMemberAsync(
        ProjectId projectId,
        UserId userId,
        CancellationToken cancellationToken)
    {
        var existing = await _membershipRepository.GetMembershipAsync(projectId, userId, cancellationToken);
        if (existing is not null)
            return Result.Failure<ProjectMembership>(Error.Unexpected with
            {
                Code = "Membership.AlreadyExists",
                Name = "User is already a member of this project"
            });

        var membershipResult = ProjectMembership.Create(projectId, userId);
        if (membershipResult.IsFailure)
            return Result.Failure<ProjectMembership>(membershipResult.Error);

        var membership = membershipResult.Value;

        _membershipRepository.Add(membership);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return membership;
    }

    public async Task<Result> RemoveMemberAsync(
        ProjectId projectId,
        UserId userId,
        CancellationToken cancellationToken)
    {
        var membership = await _membershipRepository.GetMembershipAsync(projectId, userId, cancellationToken);
        if (membership is null)
            return Result.Failure(Error.NoData);

        _membershipRepository.Delete(membership);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<UserId>>> GetProjectMemberIdsAsync(
        ProjectId projectId,
        CancellationToken cancellationToken)
    {
        var memberships = await _membershipRepository.GetAllForProjectAsync(projectId, cancellationToken);
        if (memberships is null || memberships.Count == 0)
            return Result.Failure<IReadOnlyList<UserId>>(Error.NoData);

        var userIds = memberships
            .Select(m => m.UserId)
            .ToList();

        return userIds;
    }

    public async Task<Result<IReadOnlyList<ProjectMembership>>> GetAllProjectUserAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var memberships = await _membershipRepository.GetAllForUserAsync(userId, cancellationToken);
        if (memberships is null || memberships.Count == 0)
            return Result.Failure<IReadOnlyList<ProjectMembership>>(Error.NoData);

        var activeMemberships = memberships
            .Where(m => m.Project.IsActive)
            .ToList();

        return activeMemberships;
    }
}
