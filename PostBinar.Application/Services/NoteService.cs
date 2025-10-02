using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Services;

public sealed class NoteService : INoteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INoteRepository _noteRepository;

    public NoteService(IUnitOfWork unitOfWork,INoteRepository noteRepository)
    {
        _unitOfWork = unitOfWork;
        _noteRepository = noteRepository;
    }

    public async Task<NoteId> CreateAsync(ProjectId projectId, UserId authorId, string title, string? content, int? categoryId)
    {
        var note = Note.Create(projectId, authorId, title, content, categoryId);
        
        _noteRepository.Add(note.Value);
        await _unitOfWork.SaveChangesAsync();

        return note.Value.Id;
    }

    public async Task DeleteAsync(NoteId noteId)
    {
        var note =  await _noteRepository.GetByIdAsync(noteId);
        if (note != null)
        {
            _noteRepository.Delete(note);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<List<Note>> GetAllAsync(ProjectId projectId)
    {
        var notes = await _noteRepository.GetAllAsync(projectId);
        return notes;
    }

    public async Task UpdateAsync(NoteId noteId, string title, string? content, int? categoryId)
    {
        var note = await _noteRepository.GetByIdAsync(noteId);

        if (note == null)
            throw new Exception("Note not found");
        
        note.Update(title, content, categoryId);
        _noteRepository.Update(note);
        await _unitOfWork.SaveChangesAsync();
    }
}
