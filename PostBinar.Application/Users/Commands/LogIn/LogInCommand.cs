using MediatR;
using PostBinar.Application.Common.Models.Users;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Users.Commands.LogIn;

public record LogInCommand(string Email , string Password) : IRequest<Result<AccessTokenResponse>>;
