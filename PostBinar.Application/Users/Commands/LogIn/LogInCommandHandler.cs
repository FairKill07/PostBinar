using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.Users.Commands.LogIn;

public sealed class LogInCommandHandler : IRequestHandler<LogInCommand, string>
{
    private readonly IUserService _userService;
    public LogInCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<string> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var token = await _userService.Login(
            request.Email, 
            request.Password);

        return token;
    }
}
