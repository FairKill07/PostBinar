using MediatR;

namespace Application.QrCodes.Commands;

public record GenerateQrCodeCommand(Guid FileId, int Size = 10) : IRequest<byte[]>;
