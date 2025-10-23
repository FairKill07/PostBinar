using AutoMapper;
using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Application.FileStorages.Queries.GetFilesByObject;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.FileStorages.Queries.GetFilesByObject;

public sealed class GetFilesByObjectQueryHandler
    : IRequestHandler<GetFilesByObjectQuery, FileListVm>
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IMapper _mapper;

    public GetFilesByObjectQueryHandler(IFileStorageService fileStorageService, IMapper mapper)
    {
        _fileStorageService = fileStorageService;
        _mapper = mapper;
    }

    public async Task<FileListVm> Handle(GetFilesByObjectQuery request, CancellationToken cancellationToken)
    {
        var result = await _fileStorageService.GetFilesByObjectAsync(
            request.ObjectId,
            request.StorageObjectType,
            cancellationToken);

        var fileDtos = _mapper.Map<List<FileLookUpDto>>(result.Value);

        var viewModel = new FileListVm { Files = fileDtos };

        return viewModel;
    }
}
