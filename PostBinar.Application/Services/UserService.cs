using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Application.Common.Models.Users;
using PostBinar.Domain.Abstraction;
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

    public async Task<Result<AccessTokenResponse>> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
            return Result.Failure<AccessTokenResponse>(UserErrors.NotFound);

        var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
        if (!result)
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<Result<UserId>> Register(string firstName, string lastName, string email, string password, int specializationId)
    {
        var hashPassword = _passwordHasher.HashPasssword(password);

        Result<User> result = User.Create(firstName, lastName, email, hashPassword, specializationId);

        if (result.IsFailure)
        {
            return Result.Failure<UserId>(result.Error);
        }

        User user = result.Value;

        try
        {
            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
        catch (Exception exception)
            when (exception is HttpRequestException || exception is ArgumentNullException)
        {
            return Result.Failure<UserId>(UserErrors.RegistrationFailed);
        }
    }
}
