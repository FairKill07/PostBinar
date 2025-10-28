using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;


namespace PostBinar.Application.Tasks.Commands.AssignTask;

public sealed class AssignTaskCommandHandler : IRequestHandler<AssignTaskCommand, Result>
{
    private readonly ITasksService _tasksService;
    public AssignTaskCommandHandler(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    public Task<Result> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
    {
        return _tasksService.AssignTaskToUserAsync(request.TaskItemId, request.UserId, request.CancellationToken);
    }
}
