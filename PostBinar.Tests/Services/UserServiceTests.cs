using System.Threading.Tasks;
using Moq;
using Xunit;
using PostBinar.Application.Services;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Users;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IJwtProvider> _jwtProviderMock = new();

    private readonly UserService _sut;

    public UserServiceTests()
    {
        _sut = new UserService(_userRepoMock.Object, _passwordHasherMock.Object, _unitOfWorkMock.Object, _jwtProviderMock.Object);
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenPasswordIsCorrect()
    {
        var user = User.Create("John", "Doe", "test@mail.com", "hashed", 1).Value;
        _userRepoMock.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(h => h.VerifyHashedPassword(user.PasswordHash, "1234")).Returns(true);
        _jwtProviderMock.Setup(j => j.GenerateToken(user)).Returns("jwt_token");

        var token = await _sut.Login(user.Email, "1234");

        Assert.Equal("jwt_token", token);
    }

    [Fact]
    public async Task Register_ShouldSaveUser()
    {
        _passwordHasherMock.Setup(h => h.HashPasssword("1234")).Returns("hashed");

        var id = await _sut.Register("John", "Doe", "mail@mail.com", "1234", 1);

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotEqual(default, id);
    }
}
