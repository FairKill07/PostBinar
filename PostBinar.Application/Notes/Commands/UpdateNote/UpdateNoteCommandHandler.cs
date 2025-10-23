using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Notes.Commands.UpdateNote;

public sealed class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Result>
{
    private readonly INoteService _noteService;

    public UpdateNoteCommandHandler(INoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task<Result> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var result = await _noteService.UpdateAsync(
            noteId: request.NoteId,
            title: request.Title,
            content: request.Content,
            categoryId: request.CategoryId,
            cancellationToken:cancellationToken);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        return Result.Success();
    }
}
