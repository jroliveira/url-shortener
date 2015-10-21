using FluentAssertions;
using NUnit.Framework;
using UrlShortener.WebApi.Infrastructure.Filter;

namespace UrlShortener.WebApi.Test.Infrastructure.Filter
{
    [TestFixture]
    public class LimitTests
    {
        [TestCase("?filter[limit]=9", 9)]
        [TestCase("?FILTER[LIMIT]=9", 9)]
        public void Parse_DadaQuery_ValueDeveSerIgual(string query, int expected)
        {
            Limit limit = query;

            limit.Value.Should().Be(expected);
        }

        [TestCase("?filter[limit]=Nine")]
        [TestCase("?filter[limit]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Limit limit = query;

            limit.Should().BeNull();
        }
    }
}
