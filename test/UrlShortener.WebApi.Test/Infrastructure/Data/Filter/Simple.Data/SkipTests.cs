using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data;

namespace UrlShortener.WebApi.Test.Infrastructure.Data.Filter.Simple.Data
{
    [TestFixture]
    public class SkipTests
    {
        private Mock<WebApi.Infrastructure.Data.Filter.Simple.Data.Filter> _filterStub;

        [SetUp]
        public void SetUp()
        {
            _filterStub = new Mock<WebApi.Infrastructure.Data.Filter.Simple.Data.Filter>();
        }

        [Test]
        public void Apply_DadoFiltroSemSkip_DeveRetornar0()
        {
            var skip = new Skip();

            var actual = skip.Apply(_filterStub.Object);

            actual.Should().Be(0);
        }

        [TestCase(-1, 0)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(150, 150)]
        public void Apply_DadoFiltroComSkip_DeveRetornar(int skipValue, int expected)
        {
            _filterStub
                .Setup(p => p.Skip.Value)
                .Returns(skipValue);

            var skip = new Skip();

            var actual = skip.Apply(_filterStub.Object);

            actual.Should().Be(expected);
        }
    }
}
