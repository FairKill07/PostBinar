using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.FileStorages.Queries.GetFileDownloadUrl; 

public sealed class GetFileDownloadUrlQueryHandler : IRequestHandler<GetFileDownloadUrlQuery, string>
{
    private readonly IFileStorageService _fileStorageService;
    public GetFileDownloadUrlQueryHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }
    public async Task<string> Handle(GetFileDownloadUrlQuery request, CancellationToken cancellationToken)
    {
        return await _fileStorageService.GetFileDownloadUrlAsync(request.FileStorageId, cancellationToken);
    }
}
