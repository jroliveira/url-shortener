using System;
using System.IO;

namespace UrlShortener.WebApi.Test.Lib.Extensions
{
    public static class StringExtensions
    {
        public static string Load(this string fileName, string folder)
        {
            var projectPath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(projectPath, "Modules", folder, fileName);

            return File.ReadAllText(path);
        }
    }
}