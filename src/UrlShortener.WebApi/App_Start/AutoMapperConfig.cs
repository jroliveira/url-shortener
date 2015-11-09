using AutoMapper;
using UrlShortener.WebApi.Infrastructure.Mappings;

namespace UrlShortener.WebApi
{
    public class AutoMapperConfig
    {
        public static void RegisterProfiles()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AccountProfile>();
                cfg.AddProfile<UrlProfile>();
            });
        }
    }
}
