using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Commands.CreateNote;

public sealed class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteId>
{
    private readonly INoteService _noteService;
    public CreateNoteCommandHandler(INoteService noteService)
    {
        _noteService = noteService;
    }
    public async Task<NoteId> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteId = await _noteService.CreateAsync(request.ProjectId, request.AuthorId, request.Title, request.Content, request.CategoryId);
        return noteId;
    }
}
