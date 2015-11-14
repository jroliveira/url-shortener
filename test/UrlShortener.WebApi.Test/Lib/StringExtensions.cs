using System.IO;

namespace UrlShortener.WebApi.Test.Lib
{
    public static class StringExtensions
    {
        public static string Load(this string fileName, string folder)
        {
            var projectPath = Project.GetPath();
            var path = Path.Combine(projectPath, "Modules", folder, fileName);

            return File.ReadAllText(path);
        }
    }
}