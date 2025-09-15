using MediatR;

namespace PostBinar.Application.Users.Commands.LogIn;

public record LogInCommand(string Email , string Password) : IRequest<string>;
