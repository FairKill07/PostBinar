using System.Threading.Tasks;
using Moq;
using Xunit;
using PostBinar.Application.Services;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Authorization;

public class MembershipRoleServiceTests
{
    private readonly Mock<IMembershipRoleRepository> _roleRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly MembershipRoleService _sut;

    public MembershipRoleServiceTests()
    {
        _sut = new MembershipRoleService(_roleRepoMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task AssignRoleAsync_ShouldAddRole()
    {
        var membershipId = new ProjectMembershipId(Guid.NewGuid());
        var role = Role.User;

        await _sut.AssignRoleAsync(membershipId, role);

        _roleRepoMock.Verify(r => r.Add(It.IsAny<ProjectRole>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }

    [Fact]
    public async Task RemoveRoleAsync_ShouldDeleteRole()
    {
        var membershipId = new ProjectMembershipId(Guid.NewGuid());
        var role = Role.User;
        var roleEntity = ProjectRole.Create(membershipId, role).Value;

        _roleRepoMock.Setup(r => r.GetByIdAsync(membershipId)).ReturnsAsync(roleEntity);

        await _sut.RemoveRoleAsync(membershipId, role);

        _roleRepoMock.Verify(r => r.Delete(roleEntity), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }
}
