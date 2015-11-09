using AutoMapper;

namespace UrlShortener.WebApi.Infrastructure.Mappings
{
    public class UrlProfile : Profile
    {
        protected override void Configure()
        {
            Mapper
                .CreateMap<Models.Url.Post.Url, Entities.Url>()
                .ForMember(d => d.CreationDate, o => o.Ignore())
                .ForMember(d => d.Deleted, o => o.Ignore())
                .ForMember(d => d.Shortened, o => o.Ignore());
        }
    }
}