using MediatR;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.FileStorages.Queries.GetFilesByObject;

public sealed record GetFilesByObjectQuery (Guid ObjectId , StorageObjectType StorageObjectType) : IRequest<FileListVm>;
