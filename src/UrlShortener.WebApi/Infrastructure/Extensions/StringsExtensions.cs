using System.Collections.Generic;
using System.Text;

namespace UrlShortener.WebApi.Infrastructure.Extensions
{
    public static class StringsExtensions
    {
        public static string ToInlineMessage(this IEnumerable<string> items)
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in items)
            {
                stringBuilder.AppendLine(item);
            }

            return stringBuilder.ToString();
        }
    }
}