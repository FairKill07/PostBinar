using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.Tasks
{
    public sealed record CreateTaskRequest(
        Guid ProjectId,
        Guid AuthorId,
        int? CategoryId,
        string Title,
        string? Description,
        DateTimeOffset? Deadline,
        int Status,
        int Priority) ;
}
