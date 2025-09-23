using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.Projects;

public record UpdateProjectRequest(
    Guid OwnerId,
    Guid ProjectId,
    string Name,
    string Description);
