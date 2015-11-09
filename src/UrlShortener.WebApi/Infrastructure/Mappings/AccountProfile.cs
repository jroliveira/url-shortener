using AutoMapper;
using UrlShortener.WebApi.Entities;

namespace UrlShortener.WebApi.Infrastructure.Mappings
{
    public class AccountProfile : Profile
    {
        protected override void Configure()
        {
            Mapper
                .CreateMap<Models.Account.Post.Account, Account>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreationDate, o => o.Ignore())
                .ForMember(d => d.Deleted, o => o.Ignore());

            Mapper
                .CreateMap<Models.Url.Account, Account>()
                .ForMember(d => d.Name, o => o.Ignore())
                .ForMember(d => d.Email, o => o.Ignore())
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(d => d.CreationDate, o => o.Ignore())
                .ForMember(d => d.Deleted, o => o.Ignore());
        }
    }
}