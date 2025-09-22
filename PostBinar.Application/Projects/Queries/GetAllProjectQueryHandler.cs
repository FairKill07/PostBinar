using MediatR;
using AutoMapper;
using PostBinar.Application.Abstractions.Interfaces.Repositories;

namespace PostBinar.Application.Projects.Queries
{
    public sealed class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQuery, ProjectListVm>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public GetAllProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<ProjectListVm> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetProjectsByUserIdAsync(request.UserId, cancellationToken);

            var projectDtos = _mapper.Map<List<ProjectLookUpDto>>(projects);

            return new ProjectListVm { Projects = projectDtos };
        }
    }
}
