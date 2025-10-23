using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.TaskItems;

namespace PostBinar.Application.Tasks.Commands.CreateTask
{
    public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<TaskItemId>>
    {
        private readonly ITasksService _tasksService;
        
        public CreateTaskCommandHandler(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }
        
        public async Task<Result<TaskItemId>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _tasksService.CreateTaskAsync(
                request.ProjectId,
                request.AuthorId,
                request.CategoryId,
                request.Title,
                request.Description,
                request.Deadline,
                request.Status,
                request.Priority,
                request.CancellationToken);
            
            return task;
        }
    }
}
