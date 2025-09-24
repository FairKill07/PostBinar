using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Categorys;

namespace PostBinar.Application.Categorys.Queries.GetAllSpecialization
{
    public sealed class GetAllSpecializationQueryHandler : IRequestHandler<GetAllSpecializationQuery, SpecializationListVm>
    {
        private readonly ICategorySpecializationService _categorySpecializationService;
        public GetAllSpecializationQueryHandler(ICategorySpecializationService categorySpecializationService)
        {
            _categorySpecializationService = categorySpecializationService;
        }
        public async Task<SpecializationListVm> Handle(GetAllSpecializationQuery request, CancellationToken cancellationToken)
        {
            var specializations = await _categorySpecializationService.GetAllSpecializationsAsync();
            
            var specializationVm = specializations.Select(s => new Specialization
            {
                Id = s.Id,
                Name = s.Name,
                ColorCode = s.ColorCode
            }).ToList();
            
            return new SpecializationListVm { Specializations = specializationVm };
        }
    }
}
