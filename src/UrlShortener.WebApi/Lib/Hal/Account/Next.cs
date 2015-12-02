using Nancy;
using Nancy.Hal;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Hal.Account
{
    public class Next : NextBase<Models.Account.Get.Account>
    {
        public Next(Paged<Models.Account.Get.Account> model)
            : base(model)
        {

        }

        public Link CreateLink(NancyContext context)
        {
            var link = new Link("accounts", "/accounts/{?skip,limit}");

            return link.CreateLink("next", context.Request.Query, Parameters);
        }
    }
}