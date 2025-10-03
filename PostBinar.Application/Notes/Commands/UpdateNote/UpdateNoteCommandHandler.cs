using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.Notes.Commands.UpdateNote;

public sealed class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Unit>
{
    private readonly INoteService _noteService;

    public UpdateNoteCommandHandler(INoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        await _noteService.UpdateAsync(request.NoteId, request.Title, request.Content, request.CategoryId);
        return Unit.Value;
    }
}
