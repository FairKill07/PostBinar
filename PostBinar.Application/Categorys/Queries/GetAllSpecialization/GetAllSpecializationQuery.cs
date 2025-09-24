using MediatR;

namespace PostBinar.Application.Categorys.Queries.GetAllSpecialization;

public sealed record GetAllSpecializationQuery : IRequest<SpecializationListVm>;
