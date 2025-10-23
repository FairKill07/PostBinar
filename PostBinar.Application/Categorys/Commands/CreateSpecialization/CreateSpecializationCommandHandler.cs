using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Categorys.Commands.CreateSpecialization;

public sealed class CreateSpecializationCommandHandler
    : IRequestHandler<CreateSpecializationCommand, Result<int>>
{
    private readonly ICategorySpecializationService _categorySpecializationService;

    public CreateSpecializationCommandHandler(ICategorySpecializationService categorySpecializationService)
    {
        _categorySpecializationService = categorySpecializationService;
    }

    public async Task<Result<int>> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var result = await _categorySpecializationService.CreateSpecializationAsync(
            request.Name,
            request.ColorCode,
            request.CancellationToken);

        if (result.IsFailure)
            return Result.Failure<int>(result.Error);

        return Result.Success(result.Value.Id);
    }
}
