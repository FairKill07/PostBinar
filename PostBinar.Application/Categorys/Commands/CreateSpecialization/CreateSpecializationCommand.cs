using MediatR;

namespace PostBinar.Application.Categorys.Commands.CreateSpecialization;

public sealed record CreateSpecializationCommand(
    string Name,
    string ColorCode) : IRequest<int>;
