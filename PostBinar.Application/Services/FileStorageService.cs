using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;
using PostBinar.Application.Abstractions.Interfaces.IFileStorage;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Enums;

public class FileStorageService : IFileStorageService
{
    private readonly IFileStorage _fileStorage;
    private readonly IFileStorageRepository _repository;
    private readonly IFileHelper _helper;

    public FileStorageService(
        IFileStorage fileStorage,
        IFileStorageRepository repository,
        IFileHelper helper)
    {
        _fileStorage = fileStorage;
        _repository = repository;
        _helper = helper;
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
        string storageKey = _helper.GenerateObjectKey(projectId, storageObjectType, objectId, fileName);

        await _fileStorage.UploadFileAsync(projectId, objectId, storageKey, fileStream, storageObjectType, fileName, mimeType, size, cancellationToken);

        var fileEntity = _helper.CreateStoredFile(projectId, storageObjectType, objectId, fileName, storageKey, size, mimeType);
        _repository.Add(fileEntity);

        return fileEntity;
    }

    public async Task<string> GetFileDownloadUrlAsync(
        FileStorageId fileStorageId, 
        CancellationToken cancellationToken)
    {
        var file = await _repository.GetByIdAsync(fileStorageId)
                   ?? throw new InvalidOperationException("File not found");

        return await _fileStorage.GetFileDownloadUrlAsync(file.StorageKey, cancellationToken);
    }

    public async Task<bool> DeleteFileAsync(
        FileStorageId fileStorageId, 
        CancellationToken cancellationToken)
    {
        var file = await _repository.GetByIdAsync(fileStorageId);
        if (file == null) 
            return false;

        var deleted = await _fileStorage.DeleteFileAsync(file.StorageKey, cancellationToken);
        if (deleted)
        {
            _repository.Delete(file);
        }

        return deleted;
    }

    public async Task<List<FileStorage>> GetFilesByObjectAsync(
        ProjectId objectId, 
        StorageObjectType storageObjectType)
    {
        return await _repository.GetByObjectAsync(objectId, storageObjectType);
    }
}
