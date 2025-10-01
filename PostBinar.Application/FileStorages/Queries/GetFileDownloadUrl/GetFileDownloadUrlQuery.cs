using MediatR;
using PostBinar.Domain.FileStorages;

namespace PostBinar.Application.FileStorages.Queries.GetFileDownloadUrl;

public sealed record GetFileDownloadUrlQuery(FileStorageId FileStorageId) : IRequest<string>;
