using System.IO;
using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;
using YamlDotNet.Serialization;


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
            var deserializer = new Deserializer();
            var serializer = new JsonSerializer();

            var file = _pathProvider.GetRootPath() + @"docs\swagger.yaml";

            using (var streamReader = new StreamReader(file))
            {
                var yamlObject = deserializer.Deserialize(streamReader);

                using (var stringWriter = new StringWriter())
                {
                    using (var jsonWriter = new JsonTextWriter(stringWriter))
                    {
                        serializer.Serialize(jsonWriter, yamlObject);

                        var json = stringWriter.ToString();

                        return json;
                    }
                }
            }
        }
    }
}