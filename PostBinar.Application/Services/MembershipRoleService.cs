using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Enums;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Application.Services;

public sealed class MembershipRoleService : IMembershipRoleService
{
    private readonly IMembershipRoleRepository _membershipRoleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public object ProjectsRole { get; private set; }

    public MembershipRoleService(IMembershipRoleRepository membershipRoleRepository, IUnitOfWork unitOfWork)
    {
        _membershipRoleRepository = membershipRoleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AssignRoleAsync(ProjectMembershipId membershipId, Role role)
    {
        var projectRoleResult = Domain.Authorization.ProjectRole.Create(membershipId, role);

        _membershipRoleRepository.Add(projectRoleResult.Value);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task RemoveRoleAsync(ProjectMembershipId membershipId, Role role)
    {
        var projectRoleResult = await _membershipRoleRepository.GetByIdAsync(membershipId);
        
        _membershipRoleRepository.Delete(projectRoleResult);
        
        await _unitOfWork.SaveChangesAsync();

    }
    public Task GetRolesForMembershipAsync(ProjectMembershipId projectMembershipId)
    {
        return _membershipRoleRepository.GetRolesForMembershipAsync(projectMembershipId);
    }
}
