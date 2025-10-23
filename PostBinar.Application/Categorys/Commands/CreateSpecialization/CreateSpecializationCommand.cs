using MediatR;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Categorys.Commands.CreateSpecialization;

public sealed record CreateSpecializationCommand(
    string Name,
    string ColorCode,
    CancellationToken CancellationToken) : IRequest<Result<int>>;
