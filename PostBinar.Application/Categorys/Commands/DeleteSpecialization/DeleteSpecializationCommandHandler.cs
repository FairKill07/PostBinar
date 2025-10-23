using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Categorys.Commands.DeleteSpecialization;

public sealed class DeleteSpecializationCommandHandler
    : IRequestHandler<DeleteSpecializationCommand, Result>
{
    private readonly ICategorySpecializationService _categorySpecializationService;

    public DeleteSpecializationCommandHandler(ICategorySpecializationService categorySpecializationService)
    {
        _categorySpecializationService = categorySpecializationService;
    }

    public async Task<Result> Handle(DeleteSpecializationCommand request, CancellationToken cancellationToken)
    {
        var result = await _categorySpecializationService.DeleteSpecializationAsync(
            request.SpecializationId,
            request.CancellationToken);

        return result;
    }
}
