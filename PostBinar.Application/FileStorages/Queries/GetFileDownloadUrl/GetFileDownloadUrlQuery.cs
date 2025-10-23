using MediatR;
using PostBinar.Application.Common.Models.FileStorage;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.FileStorages;

namespace PostBinar.Application.FileStorages.Queries.GetFileDownloadUrl;

public sealed record GetFileDownloadUrlQuery(
    FileStorageId FileStorageId,
    CancellationToken CancellationToken) : IRequest<Result<FileUrlResponse>>;
