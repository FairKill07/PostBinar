using System.Xml.Linq;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Application.Repositories;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<UserId> Register(string firstName, string lastName, string email, string password, int specializationId)
    {
        var hashPassword = _passwordHasher.HashPasssword(password);

        var user = User.Create(firstName, lastName, email, password, specializationId);

        _userRepository.Add(user.Value);

        await _unitOfWork.SaveChangesAsync();

        return user.Value.Id;
    }
}
