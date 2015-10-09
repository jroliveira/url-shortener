using Nancy;

namespace UrlShortener.WebApi.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => "running";
        }
    }
}