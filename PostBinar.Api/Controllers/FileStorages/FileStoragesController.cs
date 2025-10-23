using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.FileStorages.Commands.UploadFile;
using PostBinar.Application.FileStorages.Queries.GetFileDownloadUrl;
using PostBinar.Application.FileStorages.Queries.GetFilesByObject;
using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;

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
            Size: file.Length,
            cancellationToken
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

    private async Task<IActionResult> GetFilesByObjectInternal(
        Guid objectId,
        StorageObjectType type,
        CancellationToken cancellationToken)
    {
        var query = new GetFilesByObjectQuery(objectId, type,cancellationToken);
        var files = await _mediator.Send(query, cancellationToken);
        return Ok(files);
    }

    [HttpGet]
    public Task<IActionResult> GetFilesByProject(Guid projectId, CancellationToken cancellationToken) =>
        GetFilesByObjectInternal(projectId, StorageObjectType.Project, cancellationToken);

    [HttpGet]
    public Task<IActionResult> GetFilesByNote(Guid noteId, CancellationToken cancellationToken) =>
        GetFilesByObjectInternal(noteId, StorageObjectType.Note, cancellationToken);

    [HttpGet]
    public Task<IActionResult> GetFilesByTask(Guid taskId, CancellationToken cancellationToken) =>
        GetFilesByObjectInternal(taskId, StorageObjectType.Task, cancellationToken);

    [HttpGet]
    public async Task<IActionResult> GetFileDownloadUrl(Guid fileStorageId, CancellationToken cancellationToken)
    {
        var query = new GetFileDownloadUrlQuery(new FileStorageId(fileStorageId), cancellationToken);
        var url = await _mediator.Send(query, cancellationToken);
        return Ok(url);
    }
}
