using System.Text.RegularExpressions;

namespace UrlShortener.WebApi.Infrastructure.Filter
{
    public class Limit
    {
        public virtual int Value { get; protected set; }

        protected Limit()
        {
            
        }

        private Limit(int value)
        {
            Value = value;
        }

        public static implicit operator int(Limit limit)
        {
            return limit.Value;
        }

        public static implicit operator Limit(string query)
        {
            const string regex = @"filter\[limit]\=(?<limit>\d+)";
            var match = Regex.Match(query, regex, RegexOptions.IgnoreCase);

            int limit;

            if (int.TryParse(match.Groups["limit"].Value, out limit))
            {
                return new Limit(limit);
            }

            return null;
        }
    }
}