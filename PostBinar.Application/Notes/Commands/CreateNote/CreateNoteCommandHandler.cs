using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Commands.CreateNote;

public sealed class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Result<NoteId>>
{
    private readonly INoteService _noteService;

    public CreateNoteCommandHandler(INoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task<Result<NoteId>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var result = await _noteService.CreateAsync(
            projectId: request.ProjectId,
            authorId: request.AuthorId,
            title: request.Title,
            content: request.Content,
            categoryId: request.CategoryId,
            cancellationToken: cancellationToken);

        if (result.IsFailure)
            return Result.Failure<NoteId>(result.Error);

        return Result.Success(result.Value);
    }
}
