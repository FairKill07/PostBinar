using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.Categorys.Commands.CreateSpecialization;

public sealed class CreateSpecializationCommandHandler : IRequestHandler<CreateSpecializationCommand, int>
{
    private readonly ICategorySpecializationService _categorySpecializationService;
    public CreateSpecializationCommandHandler(ICategorySpecializationService categorySpecializationService)
    {
        _categorySpecializationService = categorySpecializationService;
    }
    public async Task<int> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = await _categorySpecializationService.CreateSpecializationAsync(request.Name, request.ColorCode);
        return specialization.Id;
    }
}
