using AutoMapper;

namespace UrlShortener.WebApi.Lib.Mappings
{
    public class AccountProfile : Profile
    {
        protected override void Configure()
        {
            Mapper
                .CreateMap<Models.Account.Post.Account, Entities.Account>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreationDate, o => o.Ignore())
                .ForMember(d => d.Deleted, o => o.Ignore());

            Mapper
                .CreateMap<Models.Account.Put.Account, Entities.Account>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreationDate, o => o.Ignore())
                .ForMember(d => d.Deleted, o => o.Ignore());

            Mapper
                .CreateMap<Entities.Account, Models.Account.Put.Account>();
        }
    }
}