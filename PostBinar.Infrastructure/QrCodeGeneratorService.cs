using PostBinar.Application.Abstractions.Interfaces;
using QRCoder;

namespace PostBinar.Infrastructure;

public class QrCodeGeneratorService : IQRCodeGenerator
{
    public byte[] GenerateQrCode(string text, int pixelsPerModule = 10)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

        var qrCode = new PngByteQRCode(qrCodeData);
        return qrCode.GetGraphic(pixelsPerModule);
    }
}
