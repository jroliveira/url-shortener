using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UrlShortener.WebApi.Infrastructure.Filter
{
    public class Order
    {
        private readonly ICollection<string> _properties;

        public Sorts Sorts { get; protected set; }
        public string Property { get { return _properties.First(); } }

        public Order(ICollection<string> properties, Sorts sorts)
        {
            _properties = properties;
            Sorts = sorts;
        }

        public static implicit operator Order(string query)
        {
            var properties = GetProperties(query);

            if (properties == null)
            {
                return null;
            }

            var sorts = GetSorts(query);

            return new Order(properties, sorts);
        }

        private static Sorts GetSorts(string query)
        {
            var sortsTypes = new Dictionary<string, Sorts>
            {
                { "ASC", Sorts.Asc },
                { "DESC", Sorts.Desc }
            };

            const string regex = @"filter\[order]\=(?<property>\w+)(,(%20)?(?<property>\w+))*%20(?<sorts>ASC|DESC)";

            var match = Regex.Match(query, regex);
            var sortsType = match.Groups["sorts"].Value;

            if (string.IsNullOrEmpty(sortsType))
            {
                return Sorts.Asc;
            }

            return sortsTypes[sortsType];
        }

        private static ICollection<string> GetProperties(string query)
        {
            const string regex = @"filter\[order]\=(?<property>\w+)(,(%20)?(?<property>\w+))*%20(?<sorts>ASC|DESC)";
            var matches = Regex.Matches(query, regex);

            var properties = new List<string>();

            foreach (Match m in matches)
            {
                var range = from object capture in m.Groups["property"].Captures
                            select capture.ToString();

                properties.AddRange(range);
            }

            if (!properties.Any())
            {
                return null;
            }

            return properties;
        }
    }
}