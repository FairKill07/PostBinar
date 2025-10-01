using MediatR;
using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.FileStorages.Commands.UploadFile;

public sealed record UploadFileCommand(
        ProjectId ProjectId,
        Guid ObjectId,
        Stream FileStream,
        StorageObjectType StorageObjectType,
        string FileName,
        string? MimeType,
        long Size) : IRequest<FileStorageId>;
