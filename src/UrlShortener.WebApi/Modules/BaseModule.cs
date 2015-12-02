using Nancy;
using UrlShortener.Infrastructure.Data.Filter.Simple.Data;

namespace UrlShortener.WebApi.Modules
{
    public abstract class BaseModule : NancyModule
    {
        public Filter QueryStringFilter { get { return new Filter(Request.Url.Query); } }

        protected BaseModule(string modulePath)
            : base(modulePath)
        {
            Options["/"] = _ => HttpStatusCode.OK;
        }

        protected BaseModule()
        {

        }
    }
}