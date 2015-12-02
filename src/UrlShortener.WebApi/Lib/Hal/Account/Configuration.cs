using Nancy.Hal;
using Nancy.Hal.Configuration;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Hal.Account
{
    public class Configuration
    {
        public static void Setup(HalConfiguration config)
        {
            config
                .For<Models.Account.Get.Account>()
                .Links(model => new Link("self", "/accounts/{id}").CreateLink(model))
                .Links(model => new Link("urls", "/accounts/{id}/urls").CreateLink(model));

            config
                .For<Paged<Models.Account.Get.Account>>()
                .Embeds("accounts", o => o.Data)
                .Links(model => new Link("accounts", "/accounts").CreateLink("self"))
                .Links((model, context) => new Next(model).CreateLink(context), Next.Predicate)
                .Links((model, context) => new Prev(model).CreateLink(context), Prev.Predicate);
        }
    }
}