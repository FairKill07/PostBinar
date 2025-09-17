using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Users.Commands.LogIn;
using PostBinar.Application.Users.Commands.Register;

namespace PostBinar.Api.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(
                FirstName: request.FirstName,
                LastName: request.LastName,
                Email: request.Email,
                Password: request.Password,
                SpecializationId: request.SpecializationId
            );

            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LogInUserRequest request, CancellationToken cancellationToken)
        {
            var command = new LogInCommand(
                Email: request.Email,
                Password: request.Password
            );

            var token = await _mediator.Send(command);

            HttpContext.Response.Cookies.Append("Cookies-PostBinar", token);

            return Ok(token);
        }
    }
}
