using Nancy.Hal;
using Nancy.Hal.Configuration;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Hal.Url
{
    public class Configuration
    {
        public static void Setup(HalConfiguration config)
        {
            config
                .For<Models.Url.Get.Url>()
                .Links(model => new Link("self", "/urls/{id}").CreateLink(model))
                .Links(model => new Link("account", "/accounts/{id}").CreateLink(model.Account));

            config
                .For<Paged<Models.Url.Get.Url>>()
                .Embeds("urls", o => o.Data)
                .Links(model => new Link("urls", "/urls").CreateLink("self"))
                .Links((model, context) => new Next(model).CreateLink(context), Next.Predicate)
                .Links((model, context) => new Prev(model).CreateLink(context), Prev.Predicate);
        }
    }
}