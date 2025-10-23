using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Abstractions.Interfaces.Repositories;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Notes;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Services;

public sealed class NoteService : INoteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INoteRepository _noteRepository;

    public NoteService(IUnitOfWork unitOfWork, INoteRepository noteRepository)
    {
        _unitOfWork = unitOfWork;
        _noteRepository = noteRepository;
    }

    public async Task<Result<NoteId>> CreateAsync(
        ProjectId projectId,
        UserId authorId,
        string title,
        string? content,
        int? categoryId,
        CancellationToken cancellationToken)
    {
        var noteResult = Note.Create(projectId, authorId, title, content, categoryId);
        if (noteResult.IsFailure)
            return Result.Failure<NoteId>(noteResult.Error);

        var note = noteResult.Value;
        _noteRepository.Add(note);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return note.Id;
    }

    public async Task<Result> UpdateAsync(
        NoteId noteId,
        string title,
        string? content,
        int? categoryId,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(noteId);
        if (note is null)
            return Result.Failure(NoteErrors.NotFound);

        var updateResult = note.Update(title, content, categoryId);
        if (updateResult.IsFailure)
            return Result.Failure(NoteErrors.FailedUpdate);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(
        NoteId noteId,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(noteId, cancellationToken);
        if (note is null)
            return Result.Failure(NoteErrors.NotFound);

        _noteRepository.Delete(note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<Note>>> GetAllAsync(
        ProjectId projectId,
        CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetAllAsync(projectId, cancellationToken);
        if (notes is null || notes.Count == 0)
            return Result.Failure<IReadOnlyList<Note>>(Error.NoData);

        return Result.Success<IReadOnlyList<Note>>(notes);
    }
}
