using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.Notes.Commands.DeleteNote;

public sealed class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
{
    private readonly INoteService _noteService;
    public DeleteNoteCommandHandler(INoteService noteService)
    {
        _noteService = noteService;
    }
    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        await _noteService.DeleteAsync(request.NoteId);
        return true;
    }
}
