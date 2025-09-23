namespace PostBinar.Api.Controllers.ProjectMemberships;

public sealed record AddMemberRequest(
    Guid ProjectId,
    Guid UserId,
    string Role);
