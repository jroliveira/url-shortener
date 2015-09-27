using AutoMapper;

namespace UrlShortener.WebApi.Lib.Mappings
{
    public class UrlProfile : Profile
    {
        protected override void Configure()
        {
            Mapper
                .CreateMap<Models.Url, Domain.Entities.Url>()
                .ForMember(d => d.CreationDate, o => o.Ignore())
                .ForMember(d => d.Deleted, o => o.Ignore())
                .ForMember(d => d.Shortened, o => o.Ignore());
        }
    }
}