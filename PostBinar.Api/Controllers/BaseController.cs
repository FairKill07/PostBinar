using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator = null!;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.Value is not null ? this.Ok(result.Value) : this.NotFound();
        }

        return this.HandleError(result.Error);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
        {
            return this.Ok();
        }

        return this.HandleError(result.Error);
    }
    private IActionResult HandleError(Error error)
    {
        return error switch
        {
            NotFoundError => this.NotFound(error),
            UnauthorizedError => this.Unauthorized(error),
            _ => this.BadRequest(error)
        };
    }
}