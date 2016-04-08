using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Simple.Data;
using UrlShortener.Infrastructure.Data.Filter.Simple.Data;

namespace UrlShortener.Test.Infrastructure.Data.Filter.Simple.Data
{
    [TestFixture]
    public class OrderDirectionTests
    {
        private Mock<UrlShortener.Infrastructure.Data.Filter.Simple.Data.Filter> _filterStub;

        [SetUp]
        public void SetUp()
        {
            _filterStub = new Mock<UrlShortener.Infrastructure.Data.Filter.Simple.Data.Filter>();
        }

        [TestCase(Restful.Query.Filter.Filters.Ordering.OrderByDirection.Descending, OrderByDirection.Descending)]
        [TestCase(Restful.Query.Filter.Filters.Ordering.OrderByDirection.Ascending, OrderByDirection.Ascending)]
        public void Apply_DadoFiltroComOrderSorts_DeveRetornar(Restful.Query.Filter.Filters.Ordering.OrderByDirection sorts, OrderByDirection expected)
        {
            _filterStub
                .Setup(p => p.OrderBy)
                .Returns(new Restful.Query.Filter.Filters.Ordering.OrderBy(new List<Restful.Query.Filter.Filters.Ordering.Field>
                {
                    new Restful.Query.Filter.Filters.Ordering.Field("", sorts)
                }));

            var orderDirection = new OrderDirection();

            var actual = orderDirection.Apply(_filterStub.Object);

            actual.Should().Be(expected);
        }
    }
}
