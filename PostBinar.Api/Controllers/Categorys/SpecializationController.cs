using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Categorys.Commands.CreateSpecialization;
using PostBinar.Application.Categorys.Commands.DeleteSpecialization;
using PostBinar.Application.Categorys.Queries.GetAllSpecialization;

namespace PostBinar.Api.Controllers.Categorys;

public class SpecializationController :BaseController
{
    private readonly IMediator _mediator;
    public SpecializationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpecializationRequest request , CancellationToken cancellationToken)
    {
        var specializationId = await _mediator.Send(new CreateSpecializationCommand(
            Name: request.Name,
            ColorCode: request.ColorCode), cancellationToken);
        
        return Ok(specializationId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteSpecializationCommnad(id), cancellationToken);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpecializations(CancellationToken cancellationToken)
    {
        var specializations = await _mediator.Send(new GetAllSpecializationQuery(), cancellationToken);
        return Ok(specializations);
    }
    
}
