﻿using System.IO;
using Nancy;

namespace UrlShortener.WebApi.Modules
{
    public class DocsModule : NancyModule
    {
        private readonly IRootPathProvider _rootPathProvider;

        public DocsModule(IRootPathProvider rootPathProvider)
            : base("api-docs")
        {
            _rootPathProvider = rootPathProvider;

            Get["/"] = Index;
        }

        private Response Index(dynamic _)
        {
            var rootPath = _rootPathProvider.GetRootPath();
            var file = Path.Combine(rootPath, "swagger.json");

            return Response.AsFile(file, "application/json");
        }
    }
}