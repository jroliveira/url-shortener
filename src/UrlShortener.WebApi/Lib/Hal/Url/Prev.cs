using Nancy;
using Nancy.Hal;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Hal.Url
{
    public class Prev : PrevBase<Models.Url.Get.Url>
    {
        public Prev(Paged<Models.Url.Get.Url> model)
            : base(model)
        {

        }

        public Link CreateLink(NancyContext context)
        {
            var link = new Link("urls", "/urls/{?skip,limit}");

            return link.CreateLink("prev", context.Request.Query, Parameters);
        }
    }
}