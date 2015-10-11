using Nancy;
using Nancy.Responses;

namespace UrlShortener.WebApi.Modules
{
    public class DocsModule : NancyModule
    {
        private readonly IRootPathProvider _pathProvider;

        public DocsModule(IRootPathProvider pathProvider)
            : base("api-docs")
        {
            _pathProvider = pathProvider;

            Get["/"] = _ => Index();
        }

        private Response Index()
        {
            var file = _pathProvider.GetRootPath() + "docs\\swagger.json";

            return new GenericFileResponse(file, "application/json");
        }
    }
}