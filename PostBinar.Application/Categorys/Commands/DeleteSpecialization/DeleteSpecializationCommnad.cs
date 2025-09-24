using MediatR;

namespace PostBinar.Application.Categorys.Commands.DeleteSpecialization;

public sealed record DeleteSpecializationCommnad (int SpecializationId) : IRequest<Unit>;
