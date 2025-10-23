using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Categorys.Commands.CreateSpecialization;
using PostBinar.Application.Categorys.Commands.DeleteSpecialization;
using PostBinar.Application.Categorys.Queries.GetAllSpecialization;

namespace PostBinar.Api.Controllers.Categorys
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public sealed class SpecializationController : BaseController
    {
        private readonly IMediator _mediator;

        public SpecializationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpecializationRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateSpecializationCommand(
                Name: request.Name,
                ColorCode: request.ColorCode,
                cancellationToken
            );

            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteSpecializationCommand(id, cancellationToken);

            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecializations(CancellationToken cancellationToken)
        {
            var query = new GetAllSpecializationQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
