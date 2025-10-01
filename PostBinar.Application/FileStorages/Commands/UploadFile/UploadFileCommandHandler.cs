using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.FileStorages;

namespace PostBinar.Application.FileStorages.Commands.UploadFile;

public sealed class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, FileStorageId>
{
    private readonly IFileStorageService _fileStorageService;
    public UploadFileCommandHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }
    public async Task<FileStorageId> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var file = await _fileStorageService.UploadFileAsync(
            request.ProjectId,
            request.ObjectId,
            request.FileStream,
            request.StorageObjectType,
            request.FileName,
            request.MimeType,
            request.Size,
            cancellationToken);
        return file.Id;
    }
}
