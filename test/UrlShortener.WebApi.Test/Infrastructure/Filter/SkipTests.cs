using FluentAssertions;
using NUnit.Framework;
using UrlShortener.WebApi.Infrastructure.Filter;

namespace UrlShortener.WebApi.Test.Infrastructure.Filter
{
    [TestFixture]
    public class SkipTests
    {
        [TestCase("?filter[skip]=9", 9)]
        [TestCase("?FILTER[SKIP]=9", 9)]
        public void Parse_DadaQuery_ValueDeveSerIgual(string query, int expected)
        {
            Skip skip = query;

            skip.Value.Should().Be(expected);
        }

        [TestCase("?filter[skip]=Nine")]
        [TestCase("?filter[skip]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Skip skip = query;

            skip.Should().BeNull();
        }
    }
}