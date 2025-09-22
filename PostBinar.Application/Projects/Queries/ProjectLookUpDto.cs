using AutoMapper;
using PostBinar.Application.Common.Mappings;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Projects.Queries;

public sealed class ProjectLookUpDto : IMapWith<Project>
{
    public required ProjectId ProjectId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<ProjectMembership, ProjectLookUpDto>()
            .ForMember(dto => dto.ProjectId,
                opt => opt.MapFrom(m => m.Project.Id))
            .ForMember(dto => dto.Name,
                opt => opt.MapFrom(m => m.Project.Name))
            .ForMember(dto => dto.Description,
                opt => opt.MapFrom(m => m.Project.Description))
            .ForMember(dto => dto.CreatedAt,
                opt => opt.MapFrom(m => m.Project.CreatedAt));

}
