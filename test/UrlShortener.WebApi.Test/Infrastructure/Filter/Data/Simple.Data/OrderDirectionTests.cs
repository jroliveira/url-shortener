using FluentAssertions;
using Moq;
using NUnit.Framework;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Filter.Data.Simple.Data;
using UrlShortener.WebApi.Infrastructure.Filter.Order;
using Infra = UrlShortener.WebApi.Infrastructure.Filter;

namespace UrlShortener.WebApi.Test.Infrastructure.Filter.Data.Simple.Data
{
    [TestFixture]
    public class OrderDirectionTests
    {
        private Mock<Infra.Filter> _filterStub;

        [SetUp]
        public void SetUp()
        {
            _filterStub = new Mock<Infra.Filter>();
        }

        [TestCase(Sorts.Desc, OrderByDirection.Descending)]
        [TestCase(Sorts.Asc, OrderByDirection.Ascending)]
        public void Apply_DadoFiltroComOrderSorts_DeveRetornar(Sorts sorts, OrderByDirection expected)
        {
            _filterStub
                .Setup(p => p.Order.Sorts)
                .Returns(sorts);

            var orderDirection = new OrderDirection();

            var actual = orderDirection.Apply(_filterStub.Object);

            actual.Should().Be(expected);
        }
    }
}
