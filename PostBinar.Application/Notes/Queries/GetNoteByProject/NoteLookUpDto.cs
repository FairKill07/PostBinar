using AutoMapper;
using PostBinar.Application.Common.Mappings;
using PostBinar.Domain.Notes;

namespace PostBinar.Application.Notes.Queries.GetNoteByProject;

public sealed class NoteLookUpDto : IMapWith<Note>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<Note, NoteLookUpDto>()
            .ForMember(dto => dto.Id,
                opt => opt.MapFrom(n => n.Id.Value))
            .ForMember(dto => dto.Title,
                opt => opt.MapFrom(n => n.Title))
            .ForMember(dto => dto.Content,
                opt => opt.MapFrom(n => n.Content))
            .ForMember(dto => dto.CreatedAt,
                opt => opt.MapFrom(n => n.CreatedAt))
            .ForMember(dto => dto.UpdatedAt,
                opt => opt.MapFrom(n => n.UpdatedAt));


}
