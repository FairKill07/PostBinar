using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Tasks.Commands.CreateTask;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.Tasks
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TaskController : BaseController
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateTaskCommand(
                ProjectId: new ProjectId (request.ProjectId),
                AuthorId: new UserId(request.AuthorId),
                CategoryId: request.CategoryId,
                Title: request.Title,
                Description: request.Description,
                Deadline: request.Deadline,
                Status: (TaskItemStatus)request.Status,
                Priority: (TaskItemPriority)request.Priority
            );
            var taskId = await _mediator.Send(command, cancellationToken);
            return HandleResult(taskId);
        }
    }
}
