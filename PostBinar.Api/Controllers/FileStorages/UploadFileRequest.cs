using Microsoft.AspNetCore.Mvc;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;

namespace PostBinar.Api.Controllers.FileStorages;

public sealed record UploadFileRequest(
        Guid ProjectId,
        Guid ObjectId)
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; } = null!;
};


