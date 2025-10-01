using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.FileStorages.Commands.UploadFile;
using PostBinar.Domain.Enums;

namespace PostBinar.Api.Controllers.FileStorages;


[ApiController]
[Route("api/[controller]/[action]")]
public sealed class FileStoragesController : BaseController
{
    private readonly IMediator _mediator;
    public FileStoragesController (IMediator mediator)
    {
        _mediator = mediator;
    }
    private async Task<IActionResult> UploadFileInternal(
        UploadFileRequest request,
        StorageObjectType type,
        CancellationToken cancellationToken)
    {
        var file = request.File;
        var command = new UploadFileCommand(
            ProjectId: request.ProjectId,
            ObjectId: request.ObjectId,
            FileStream: file.OpenReadStream(),
            StorageObjectType: type,
            FileName: file.FileName,
            MimeType: file.ContentType,
            Size: file.Length
        );

        var fileId = await _mediator.Send(command, cancellationToken);
        return Ok(fileId);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public Task<IActionResult> UploadFileForProject([FromForm] UploadFileRequest request, CancellationToken cancellationToken) =>
        UploadFileInternal(request, StorageObjectType.Project, cancellationToken);

    [HttpPost]
    [Consumes("multipart/form-data")]
    public Task<IActionResult> UploadFileForNote([FromForm] UploadFileRequest request, CancellationToken cancellationToken) =>
        UploadFileInternal(request, StorageObjectType.Note, cancellationToken);

    [HttpPost]
    [Consumes("multipart/form-data")]
    public Task<IActionResult> UploadFileForTask([FromForm] UploadFileRequest request, CancellationToken cancellationToken) =>
        UploadFileInternal(request, StorageObjectType.Task, cancellationToken);


}
