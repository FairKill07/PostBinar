namespace PostBinar.Application.Abstractions.Interfaces;

public interface IQRCodeGenerator
{
    byte[] GenerateQrCode(string text, int pixelsPerModule = 10);
}
