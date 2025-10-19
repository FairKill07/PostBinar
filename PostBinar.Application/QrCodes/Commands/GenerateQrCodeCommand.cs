using MediatR;

namespace Application.QrCodes.Commands;

public record GenerateQrCodeCommand(string Text, int Size = 10) : IRequest<byte[]>;
