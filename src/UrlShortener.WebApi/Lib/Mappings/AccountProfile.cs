using AutoMapper;
using UrlShortener.WebApi.Domain.Entities;

namespace UrlShortener.WebApi.Lib.Mappings
{
    public class AccountProfile : Profile
    {
        protected override void Configure()
        {
            Mapper
                .CreateMap<Models.Account, Account>()
                .ForMember(d => d.CreationDate, o => o.Ignore())
                .ForMember(d => d.Deleted, o => o.Ignore());

            Mapper
                .CreateMap<Account, Models.Account>();
        }
    }
}