using System;
using System.IO;
using System.Linq;

namespace UrlShortener.WebApi.Test.Lib
{
    public class Project
    {
        public static string GetPath()
        {
            var startupPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + Path.DirectorySeparatorChar;
            var pathItems = startupPath.Split(Path.DirectorySeparatorChar);
            var projectPath = string.Join(Path.DirectorySeparatorChar.ToString(), pathItems.Take(pathItems.Length - 3));

            return projectPath;
        }
    }
}