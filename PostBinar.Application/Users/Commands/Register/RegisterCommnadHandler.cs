using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Users.Commands.Register;

public sealed class RegisterCommnadHandler : IRequestHandler<RegisterCommand, UserId>
{
    private readonly IUserService _userService;
    public RegisterCommnadHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<UserId> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password, 
            request.SpecializationId);
        
        return userId;
    }
}
