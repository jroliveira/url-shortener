using System;
using System.IO;
using System.Linq;

namespace UrlShortener.WebApi.Test.Lib
{
    public static class StringExtensions
    {
        public static string Load(this string fileName, string folder)
        {
            return File.ReadAllText(GetPath(fileName, folder));
        }

        private static string GetPath(string file, string folder)
        {
            var startupPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + Path.DirectorySeparatorChar;
            var pathItems = startupPath.Split(Path.DirectorySeparatorChar);
            var projectPath = string.Join(Path.DirectorySeparatorChar.ToString(), pathItems.Take(pathItems.Length - 3));

            return Path.Combine(projectPath, "Modules", folder, file);
        }
    }
}