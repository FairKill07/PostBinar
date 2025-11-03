using MediatR;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.FileStorages;

namespace Application.QrCodes.Commands;

public class GenerateQrCodeHandler : IRequestHandler<GenerateQrCodeCommand, byte[]>
{
    private readonly IQRCodeGenerator _qrCodeGenerator;
    private readonly IFileStorageService _fileStorageService;

    public GenerateQrCodeHandler(IQRCodeGenerator qrCodeGenerator, IFileStorageService fileStorageService)
    {
        _qrCodeGenerator = qrCodeGenerator;
        _fileStorageService = fileStorageService;
    }

    public async Task<byte[]> Handle(GenerateQrCodeCommand request, CancellationToken cancellationToken)
    {
        var url = await _fileStorageService.GetFileDownloadUrlAsync(new FileStorageId(request.FileId), cancellationToken);
        
        var qrBytes = _qrCodeGenerator.GenerateQrCode(url.Value.Url, request.Size);

        return qrBytes;
    }
}
