using MediatR;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Notes.Queries.GetNoteByProject;

public sealed record GetNoteByProjectQuery (ProjectId ProjectId) : IRequest<NoteListVm>;
