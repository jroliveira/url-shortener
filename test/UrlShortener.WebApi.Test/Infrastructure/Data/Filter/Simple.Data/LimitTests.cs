using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data;

namespace UrlShortener.WebApi.Test.Infrastructure.Data.Filter.Simple.Data
{
    [TestFixture]
    public class LimitTests
    {
        private Mock<WebApi.Infrastructure.Data.Filter.Simple.Data.Filter> _filterStub;

        [SetUp]
        public void SetUp()
        {
            _filterStub = new Mock<WebApi.Infrastructure.Data.Filter.Simple.Data.Filter>();
        }

        [Test]
        public void Apply_DadoFiltroSemLimit_DeveRetornar100()
        {
            var limit = new Limit();

            var actual = limit.Apply(_filterStub.Object);

            actual.Should().Be(100);
        }

        [TestCase(-1, 100)]
        [TestCase(0, 100)]
        [TestCase(1, 1)]
        [TestCase(150, 150)]
        public void Apply_DadoFiltroComLimit_DeveRetornar(int limitValue, int expected)
        {
            _filterStub
                .Setup(p => p.Limit.Value)
                .Returns(limitValue);

            var limit = new Limit();

            var actual = limit.Apply(_filterStub.Object);

            actual.Should().Be(expected);
        }
    }
}
