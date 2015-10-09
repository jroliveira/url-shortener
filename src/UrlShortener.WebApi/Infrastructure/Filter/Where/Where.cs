using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UrlShortener.WebApi.Infrastructure.Filter.Where
{
    public class Where
    {
        public Operator Operator { get; protected set; }
        public Property Property { get; set; }

        public Where(Property property, Operator @operator)
        {
            Property = property;
            Operator = @operator;
        }

        public static implicit operator Where(string query)
        {
            const string regex = @"filter\[where]\[(?<property>\w+)\](\[(?<op>gt|lt)\])?=(?<value>[^&]*)&?";
            var match = Regex.Match(query, regex, RegexOptions.IgnoreCase);

            var property = GetProperty(match);
            if (property == null)
            {
                return null;
            }

            var @operator = GetOperator(match);
            if (@operator == null)
            {
                return null;
            }

            return new Where(property, @operator.Value);
        }

        private static Operator? GetOperator(Match match)
        {
            var operations = new Dictionary<string, Operator>
            {
                { "gt", Operator.GreaterThan },
                { "lt", Operator.LessThan }
            };

            var operation = match.Groups["op"].Value;
            if (string.IsNullOrEmpty(operation))
            {
                return null;
            }

            return operations[operation];
        }

        private static Property GetProperty(Match match)
        {
            var name = match.Groups["property"].Value;
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var value = match.Groups["value"].Value;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return new Property(name, value);
        }
    }
}