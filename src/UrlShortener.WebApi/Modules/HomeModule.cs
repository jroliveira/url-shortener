using System.Reflection;
using Nancy;

namespace UrlShortener.WebApi.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            var info = Assembly.GetExecutingAssembly().GetName();

            Get["/"] = _ => Response.AsJson(new
            {
                version = info.Version.ToString(),
                messsage = "I'm working..."
            });
        }
    }
}