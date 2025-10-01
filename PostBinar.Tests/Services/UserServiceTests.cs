using Bogus;
using Moq;
using PostBinar.Application.Services;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;

public class UserServiceBogusTests
{
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IJwtProvider> _jwtProviderMock = new();

    private readonly UserService _sut;

    private readonly Faker _faker = new();

    public UserServiceBogusTests()
    {
        _sut = new UserService(
            _userRepoMock.Object,
            _passwordHasherMock.Object,
            _unitOfWorkMock.Object,
            _jwtProviderMock.Object
        );
    }

    [Fact]
    public async Task Register_ShouldSaveUser_WithFakerData()
    {
        var fakeUser = new
        {
            FirstName = _faker.Name.FirstName(),
            LastName = _faker.Name.LastName(),
            Email = _faker.Internet.Email(),
            Password = _faker.Internet.Password(8),
            RoleId = _faker.Random.Int(1, 5)
        };

        _passwordHasherMock.Setup(h => h.HashPasssword(fakeUser.Password))
            .Returns("hashed_pw");

        // Act
        var userId = await _sut.Register(
            fakeUser.FirstName,
            fakeUser.LastName,
            fakeUser.Email,
            fakeUser.Password,
            fakeUser.RoleId
        );

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotEqual(default, userId);
    }
}
