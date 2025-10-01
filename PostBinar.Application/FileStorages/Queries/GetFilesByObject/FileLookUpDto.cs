using PostBinar.Application.Common.Mappings;
using PostBinar.Domain.FileStorages;

namespace PostBinar.Application.FileStorages.Queries.GetFilesByObject
{
    public sealed class FileLookUpDto : IMapWith<FileStorage>
    {
        public required Guid FileSorageId { get; set; }
        public required string FileName { get; set; }
        public required string MimeType { get; set; }
        public required long Size { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public void Mapping(AutoMapper.Profile profile) =>
            profile.CreateMap<FileStorage, FileLookUpDto>()
                .ForMember(dto => dto.FileSorageId,
                    opt => opt.MapFrom(f => f.Id.Value))
                .ForMember(dto => dto.FileName,
                    opt => opt.MapFrom(f => f.FileName))
                .ForMember(dto => dto.MimeType,
                    opt => opt.MapFrom(f => f.MimeType))
                .ForMember(dto => dto.Size,
                    opt => opt.MapFrom(f => f.Size))
                .ForMember(dto => dto.CreatedAt,
                    opt => opt.MapFrom(f => f.CreatedAt));
    }
}
