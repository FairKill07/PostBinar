using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Services;

public sealed class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProjectId>> CreateProjectAsync(
        string name,
        string description,
        UserId ownerId,
        CancellationToken cancellationToken)
    {
        var projectResult = Project.Create(name, description, ownerId);
        if (projectResult.IsFailure)
            return Result.Failure<ProjectId>(projectResult.Error);

        var project = projectResult.Value;

        _projectRepository.Add(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id;
    }

    public async Task<Result> UpdateProjectAsync(
        UserId ownerId,
        ProjectId projectId,
        string name,
        string description,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);
        if (project is null)
            return Result.Failure(ProjectErrors.NotFound);

        if (!project.IsOwner(ownerId))
            return Result.Failure(ProjectErrors.CannotRemoveOwner);

        var updateResult = project.Update(name, description);
        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Deactivate(ProjectId projectId, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);
        if (project is null)
            return Result.Failure(ProjectErrors.NotFound);

        var deactivateResult = project.Deactivate();
        if (deactivateResult.IsFailure)
            return Result.Failure(deactivateResult.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteProject(ProjectId projectId, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);
        if (project is null)
            return Result.Failure(ProjectErrors.NotFound);

        _projectRepository.Delete(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
