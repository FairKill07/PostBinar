using MediatR;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Tasks.Commands.AssignTask;

public sealed record AssignTaskCommand(TaskItemId TaskItemId, UserId UserId, CancellationToken CancellationToken): IRequest<Result>;