using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Application.Common.Models.FileStorage;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.FileStorages.Queries.GetFileDownloadUrl;

public sealed class GetFileDownloadUrlQueryHandler
    : IRequestHandler<GetFileDownloadUrlQuery, Result<FileUrlResponse>>
{
    private readonly IFileStorageService _fileStorageService;

    public GetFileDownloadUrlQueryHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<FileUrlResponse>> Handle(GetFileDownloadUrlQuery request, CancellationToken cancellationToken)
    {
        var result = await _fileStorageService.GetFileDownloadUrlAsync(
            request.FileStorageId,
            request.CancellationToken);

        if (result.IsFailure)
            return Result.Failure<FileUrlResponse>(result.Error);

        return Result.Success(result.Value);
    }
}
