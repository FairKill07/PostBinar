using AutoMapper;
using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;

namespace PostBinar.Application.Notes.Queries.GetNoteByProject
{
    public sealed class GetNoteByProjectQueryHandler : IRequestHandler<GetNoteByProjectQuery, NoteListVm>
    {
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;
        
        public GetNoteByProjectQueryHandler(INoteService noteService, IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        public async Task<NoteListVm> Handle(GetNoteByProjectQuery request, CancellationToken cancellationToken)
        {
            var notes =  await _noteService.GetAllAsync(request.ProjectId, cancellationToken);
            var noteDtos = _mapper.Map<List<NoteLookUpDto>>(notes.Value);
            var noteListVm = new NoteListVm { Notes = noteDtos };
            return noteListVm;
        }
    }
}
