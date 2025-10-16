using MediatR;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(
    ProjectId ProjectId, 
    UserId AuthorId, 
    int? CategoryId, 
    string Title, 
    string? Description, 
    DateTimeOffset? Deadline, 
    TaskItemStatus Status, 
    TaskItemPriority Priority) : IRequest<TaskItemId>;