using MediatR;
using PostBinar.Application.Abstractions.Interfaces;

namespace Application.QrCodes.Commands;

public class GenerateQrCodeHandler : IRequestHandler<GenerateQrCodeCommand, byte[]>
{
    private readonly IQRCodeGenerator _qrCodeGenerator;

    public GenerateQrCodeHandler(IQRCodeGenerator qrCodeGenerator)
    {
        _qrCodeGenerator = qrCodeGenerator;
    }

    public Task<byte[]> Handle(GenerateQrCodeCommand request, CancellationToken cancellationToken)
    {
        var qrBytes = _qrCodeGenerator.GenerateQrCode(request.Text, request.Size);
        return Task.FromResult(qrBytes);
    }
}
