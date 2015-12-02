using AutoMapper;
using UrlShortener.WebApi.Lib.Mappings;

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
                cfg.AddProfile<PagedProfile>();
            });
        }
    }
}
