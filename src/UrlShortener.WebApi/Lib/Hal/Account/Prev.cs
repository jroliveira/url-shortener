using Nancy;
using Nancy.Hal;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Hal.Account
{
    public class Prev : PrevBase<Models.Account.Get.Account>
    {
        public Prev(Paged<Models.Account.Get.Account> model)
            : base(model)
        {

        }

        public Link CreateLink(NancyContext context)
        {
            var link = new Link("accounts", "/accounts/{?skip,limit}");

            return link.CreateLink("prev", context.Request.Query, Parameters);
        }
    }
}