using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Services
{
    public sealed class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectId> CreateProjectAsync(string name, string description, UserId ownerId)
        {
            var project = Project.Create(name, description, ownerId);

            _projectRepository.Add(project.Value);
            
            await _unitOfWork.SaveChangesAsync();

            return project.Value.Id;
        }
        public async Task<Project> UpdateProjectAsync(UserId ownerId, ProjectId projectId, string name, string description)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                throw new KeyNotFoundException($"Project with id {projectId} not found.");

            if (!project.IsOwner(ownerId))
                throw new InvalidOperationException("Only the project owner can update the project.");

            project.Update(name, description);

            _projectRepository.Update(project);

            await _unitOfWork.SaveChangesAsync();

            return project;
        }
        public async Task Deactivate(ProjectId projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            
            if (project is null)
                throw new KeyNotFoundException($"Project with id {projectId} not found.");
            
            project.Deactivate();
            
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteProject(ProjectId projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                throw new KeyNotFoundException($"Project with id {projectId} not found.");

            _projectRepository.Delete(project);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
