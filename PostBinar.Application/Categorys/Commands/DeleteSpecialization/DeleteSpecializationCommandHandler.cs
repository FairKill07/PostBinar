using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.Categorys.Commands.DeleteSpecialization
{
    public sealed class DeleteSpecializationCommandHandler : IRequestHandler<DeleteSpecializationCommnad, Unit>
    {
        private readonly ICategorySpecializationService _categorySpecializationService;
        public DeleteSpecializationCommandHandler(ICategorySpecializationService categorySpecializationService)
        {
            _categorySpecializationService = categorySpecializationService;
        }

        public Task<Unit> Handle(DeleteSpecializationCommnad request, CancellationToken cancellationToken)
        {
            _categorySpecializationService.DeleteSpecializationAsync(request.SpecializationId);
            return Task.FromResult(Unit.Value);
        }
    }
}
