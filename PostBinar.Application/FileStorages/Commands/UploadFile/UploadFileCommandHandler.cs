using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.FileStorages;

namespace PostBinar.Application.FileStorages.Commands.UploadFile;

public sealed class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result<FileStorageId>>
{
    private readonly IFileStorageService _fileStorageService;

    public UploadFileCommandHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<FileStorageId>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var uploadResult = await _fileStorageService.UploadFileAsync(
            projectId: request.ProjectId,
            objectId: request.ObjectId,
            fileStream: request.FileStream,
            storageObjectType: request.StorageObjectType,
            fileName: request.FileName,
            mimeType: request.MimeType,
            size: request.Size,
            cancellationToken: cancellationToken);

        if (uploadResult is null)
            return Result.Failure<FileStorageId>(Error.Unexpected);

        return Result.Success(uploadResult.Value.Id);
    }
}
