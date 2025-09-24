using System.Threading.Tasks;
using Moq;
using Xunit;
using PostBinar.Application.Services;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;
using System.Collections.Generic;

public class ProjectMembershipServiceTests
{
    private readonly Mock<IProjectMembershipRepository> _membershipRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly ProjectMembershipService _sut;

    public ProjectMembershipServiceTests()
    {
        _sut = new ProjectMembershipService(_membershipRepoMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task AddMemberAsync_ShouldAddAndSave()
    {
        var projectId = new ProjectId(Guid.NewGuid());
        var userId = new UserId(Guid.NewGuid());

        _membershipRepoMock.Setup(r => r.GetMembershipAsync(projectId, userId)).ReturnsAsync((ProjectMembership)null);

        var result = await _sut.AddMemberAsync(projectId, userId);

        _membershipRepoMock.Verify(r => r.Add(It.IsAny<ProjectMembership>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.Equal(userId, result.UserId);
    }

    [Fact]
    public async Task GetProjectMemberIdsAsync_ShouldReturnList()
    {
        var projectId = new ProjectId(Guid.NewGuid());
        var members = new List<ProjectMembership>
        {
            ProjectMembership.Create(projectId, new UserId(Guid.NewGuid())).Value
        };

        _membershipRepoMock.Setup(r => r.GetAllForProjectAsync(projectId)).ReturnsAsync(members);

        var result = await _sut.GetProjectMemberIdsAsync(projectId);

        Assert.Single(result);
    }
}
