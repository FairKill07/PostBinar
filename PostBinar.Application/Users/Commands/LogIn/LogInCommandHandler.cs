using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Application.Common.Models.Users;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Users.Commands.LogIn;

public sealed class LogInCommandHandler : IRequestHandler<LogInCommand, Result<AccessTokenResponse>>
{
    private readonly IUserService _userService;
    public LogInCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<Result<AccessTokenResponse>> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var token = await _userService.Login(
            request.Email, 
            request.Password,
            request.CancellationToken);

        return token;
    }
}
