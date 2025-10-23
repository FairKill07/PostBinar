using MediatR;
using AutoMapper;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.Projects.Queries.GetAllProject
{
    public sealed class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQuery, ProjectListVm>
    {
        private readonly IProjectMembershipService _projectMembershipService;
        private readonly IMapper _mapper;
        public GetAllProjectQueryHandler(IProjectMembershipService projectMembershipService, IMapper mapper)
        {
            _projectMembershipService = projectMembershipService;
            _mapper = mapper;
        }
        public async Task<ProjectListVm> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectMembershipService.GetAllProjectUserAsync(request.UserId, cancellationToken);

            var projectDtos = _mapper.Map<List<ProjectLookUpDto>>(projects.Value);

            return new ProjectListVm { Projects = projectDtos };
        }
    }
}
