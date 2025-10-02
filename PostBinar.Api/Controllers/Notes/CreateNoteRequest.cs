using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.Notes;

public sealed record CreateNoteRequest(
    Guid ProjectId,
    Guid AuthorId,
    string Title,
    string? Content,
    int? CategoryId);
