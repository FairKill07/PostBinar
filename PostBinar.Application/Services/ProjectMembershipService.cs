using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Services;

public sealed class ProjectMembershipService : IProjectMembershipService
{
    private readonly IProjectMembershipRepository _membershipRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectMembershipService(IProjectMembershipRepository membershipRepository, IUnitOfWork unitOfWork)
    {
        _membershipRepository = membershipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProjectMembership> AddMemberAsync(ProjectId projectId, UserId userId)
    {
        var existing = await _membershipRepository.GetByUserAndProjectAsync(projectId, userId);

        var membership = ProjectMembership.Create(projectId, userId);

        _membershipRepository.Add(membership.Value);

        await _unitOfWork.SaveChangesAsync();

        return membership.Value;
    }

    public async Task RemoveMemberAsync(ProjectId projectId, UserId userId)
    {
        var membership = await _membershipRepository.GetByUserAndProjectAsync(projectId, userId);

        _membershipRepository.Delete(membership);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserId>> GetProjectMemberIdsAsync(ProjectId projectId)
    {
        var memberships = await _membershipRepository.GetAllByProjectIdAsync(projectId);
        return memberships.Select(m => m.UserId);
    }
}
