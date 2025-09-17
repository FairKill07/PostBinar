using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.Projects;

public record CreateProjectRequest(
    string Name,
    string Description,
    UserId OwnerId);