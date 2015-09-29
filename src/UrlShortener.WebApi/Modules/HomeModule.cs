using Nancy;
using Nancy.Responses;

namespace UrlShortener.WebApi.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly IRootPathProvider _pathProvider;

        public HomeModule(IRootPathProvider pathProvider)
        {
            _pathProvider = pathProvider;

            Get["/"] = _ => "running";
            Get["/swagger.json"] = _ => SwaggerJson();
        }

        private Response SwaggerJson()
        {
            var file = _pathProvider.GetRootPath() + @"\swagger.json";

            return new GenericFileResponse(file, "application/json");
        }
    }
}