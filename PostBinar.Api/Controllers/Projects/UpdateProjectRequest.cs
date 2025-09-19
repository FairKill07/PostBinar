using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.Projects;

public record UpdateProjectRequest(
    UserId OwnerId,
    ProjectId ProjectId,
    string Name,
    string Description);
