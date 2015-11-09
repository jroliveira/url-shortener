using System.Text.RegularExpressions;

namespace Restful.Query.Filter
{
    public class Skip
    {
        public virtual int Value { get; protected set; }

        protected Skip()
        {

        }

        private Skip(int value)
        {
            Value = value;
        }

        public static implicit operator int(Skip skip)
        {
            return skip.Value;
        }

        public static implicit operator Skip(string query)
        {
            const string regex = @"filter\[skip]\=(?<skip>\d+)";
            var match = Regex.Match(query, regex, RegexOptions.IgnoreCase);

            int skip;

            if (int.TryParse(match.Groups["skip"].Value, out skip))
            {
                return new Skip(skip);
            }

            return null;
        }
    }
}