using Application.QrCodes.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Api.Controllers;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QrController : BaseController
    {
        private readonly IMediator _mediator;

        public QrController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<byte[]> GenerateQr([FromQuery] string text, [FromQuery] int size = 10)
        {
            var qrBytes = await _mediator.Send(new GenerateQrCodeCommand(text, size));
            return qrBytes;
        }
    }
}
