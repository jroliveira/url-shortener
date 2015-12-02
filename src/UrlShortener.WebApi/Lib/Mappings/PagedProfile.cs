using AutoMapper;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Mappings
{
    public class PagedProfile : Profile
    {
        protected override void Configure()
        {
            Mapper
                .CreateMap(typeof(Paged<>), typeof(Paged<>));
        }
    }
}