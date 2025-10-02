using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Notes.Commands.CreateNote;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.Notes
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NoteController : BaseController
    {
        private readonly IMediator _mediator;
        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateNoteCommand(
                ProjectId: new ProjectId (request.ProjectId),
                AuthorId: new UserId(request.AuthorId),
                Title: request.Title,
                Content: request.Content,
                CategoryId: request.CategoryId
            );
            return Ok(await _mediator.Send(command));
        }

    }
}
