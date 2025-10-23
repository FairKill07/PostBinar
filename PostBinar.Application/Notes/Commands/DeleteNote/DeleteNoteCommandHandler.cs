using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;

namespace PostBinar.Application.Notes.Commands.DeleteNote;

public sealed class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Result>
{
    private readonly INoteService _noteService;

    public DeleteNoteCommandHandler(INoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task<Result> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var result = await _noteService.DeleteAsync(request.NoteId, cancellationToken);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        return Result.Success();
    }
}
