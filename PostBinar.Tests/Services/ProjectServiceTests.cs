using System.Threading.Tasks;
using Moq;
using Xunit;
using PostBinar.Application.Services;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _projectRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly ProjectService _sut;

    public ProjectServiceTests()
    {
        _sut = new ProjectService(_projectRepoMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateProjectAsync_ShouldReturnId()
    {
        var id = await _sut.CreateProjectAsync("Test", "Desc", new UserId(Guid.NewGuid()));

        _projectRepoMock.Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotEqual(default, id);
    }

    [Fact]
    public async Task UpdateProjectAsync_ShouldThrow_WhenNotOwner()
    {
        var owner = new UserId(Guid.NewGuid());
        var other = new UserId(Guid.NewGuid());
        var project = Project.Create("Test", "Desc", other).Value;

        _projectRepoMock.Setup(r => r.GetByIdAsync(project.Id)).ReturnsAsync(project);

        await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.UpdateProjectAsync(owner, project.Id, "New", "New"));
    }
}
