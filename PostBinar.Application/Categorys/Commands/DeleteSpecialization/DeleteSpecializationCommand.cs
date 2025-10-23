using MediatR;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Categorys.Commands.DeleteSpecialization;

public sealed record DeleteSpecializationCommand (
    int SpecializationId,
    CancellationToken CancellationToken) : IRequest<Result>;
