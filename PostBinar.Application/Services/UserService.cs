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

    public Task<string> Login(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<UserId> Register(string firstName, string lastName, string email, string passwordHash, int specializationId)
    {
        throw new NotImplementedException();
    }
}
