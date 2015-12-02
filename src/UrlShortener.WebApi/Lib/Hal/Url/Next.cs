using Nancy;
using Nancy.Hal;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Hal.Url
{
    public class Next : NextBase<Models.Url.Get.Url>
    {
        public Next(Paged<Models.Url.Get.Url> model)
            : base(model)
        {

        }

        public Link CreateLink(NancyContext context)
        {
            var link = new Link("urls", "/urls/{?skip,limit}");

            return link.CreateLink("next", context.Request.Query, Parameters);
        }
    }
}