using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Notes.Commands.CreateNote;
using PostBinar.Application.Notes.Commands.DeleteNote;
using PostBinar.Application.Notes.Commands.UpdateNote;
using PostBinar.Domain.Notes;
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

        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody] UpdateNoteRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateNoteCommand(
                NoteId: request.NoteId,
                Title: request.Title,
                Content: request.Content,
                CategoryId: request.CategoryId
            );
            return Ok(await _mediator.Send(command));
        }
        [HttpDelete("{noteId:guid}")]
        public async Task<IActionResult> DeleteNote(Guid noteId, CancellationToken cancellationToken)
        {
            var command = new DeleteNoteCommand(
                NoteId: new NoteId (noteId)
            );
            return Ok(await _mediator.Send(command));
        }

    }
}
