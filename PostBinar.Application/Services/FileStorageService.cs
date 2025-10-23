using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.IFileStorage;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Application.Common.Models.FileStorage;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Services;

public sealed class FileStorageService : IFileStorageService
{
    private readonly IFileStorage _fileStorage;
    private readonly IFileStorageRepository _repository;
    private readonly IFileHelper _helper;
    private readonly IUnitOfWork _unitOfWork;

    public FileStorageService(
        IFileStorage fileStorage,
        IFileStorageRepository repository,
        IFileHelper helper,
        IUnitOfWork unitOfWork)
    {
        _fileStorage = fileStorage;
        _repository = repository;
        _helper = helper;
        _unitOfWork = unitOfWork;
    }

    public async Task<FileStorage> UploadFileAsync(
        ProjectId projectId,
        Guid objectId,
        Stream fileStream,
        StorageObjectType storageObjectType,
        string fileName,
        string? mimeType,
        long size,
        CancellationToken cancellationToken)
    {
        await _fileStorage.EnsureBucketExistsAsync(cancellationToken);

        string storageKey = _helper.GenerateObjectKey(projectId, storageObjectType, objectId, fileName);

        await _fileStorage.UploadFileAsync(
            projectId,
            objectId,
            storageKey,
            fileStream,
            storageObjectType,
            fileName,
            mimeType,
            size,
            cancellationToken);

        var fileEntity = _helper.CreateStoredFile(projectId, storageObjectType, objectId, fileName, storageKey, size, mimeType);

        _repository.Add(fileEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return fileEntity;
    }

    public async Task<Result<FileUrlResponse>> GetFileDownloadUrlAsync(
        FileStorageId fileStorageId,
        CancellationToken cancellationToken)
    {
        var file = await _repository.GetByIdAsync(fileStorageId, cancellationToken);
        if (file is null)
            return Result.Failure<FileUrlResponse>(FileStorageErrors.NotFound);

        var url = await _fileStorage.GetFileDownloadUrlAsync(file.StorageKey, cancellationToken);
        var response = new FileUrlResponse(url);

        return response;
    }

    public async Task<Result> DeleteFileAsync(
        FileStorageId fileStorageId,
        CancellationToken cancellationToken)
    {
        var file = await _repository.GetByIdAsync(fileStorageId, cancellationToken);
        if (file is null)
            return Result.Failure(FileStorageErrors.NotFound);

        var deleted = await _fileStorage.DeleteFileAsync(file.StorageKey, cancellationToken);
        if (!deleted)
            return Result.Failure(Error.Unexpected);

        _repository.Delete(file);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<FileStorage>>> GetFilesByObjectAsync(
        Guid objectId,
        StorageObjectType storageObjectType,
        CancellationToken cancellationToken)
    {
        var files = await _repository.GetByObjectAsync(objectId, storageObjectType, cancellationToken);
        if (files is null || files.Count == 0)
            return Result.Failure<IReadOnlyList<FileStorage>>(Error.NoData);

        return Result.Success(files);
    }
}
