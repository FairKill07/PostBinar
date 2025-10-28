using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.Tasks;

public sealed record AssignTaskRequest (TaskItemId TaskItemId, UserId UserId);
