using MediatR;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Users.Commands.Register;

public sealed record RegisterCommand(
    string FirstName, 
    string LastName, 
    string Email, 
    string Password, 
    int SpecializationId, 
    CancellationToken CancellationToken) : IRequest<Result<UserId>>;

