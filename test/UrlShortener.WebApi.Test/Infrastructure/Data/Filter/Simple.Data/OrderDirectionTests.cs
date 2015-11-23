using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Restful.Query.Filter.Order;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data;

namespace UrlShortener.WebApi.Test.Infrastructure.Data.Filter.Simple.Data
{
    [TestFixture]
    public class OrderDirectionTests
    {
        private Mock<WebApi.Infrastructure.Data.Filter.Simple.Data.Filter> _filterStub;

        [SetUp]
        public void SetUp()
        {
            _filterStub = new Mock<WebApi.Infrastructure.Data.Filter.Simple.Data.Filter>();
        }

        [TestCase(Sorts.Desc, OrderByDirection.Descending)]
        [TestCase(Sorts.Asc, OrderByDirection.Ascending)]
        public void Apply_DadoFiltroComOrderSorts_DeveRetornar(Sorts sorts, OrderByDirection expected)
        {
            _filterStub
                .Setup(p => p.Order.Fields)
                .Returns(new List<Field> { new Field("", sorts) });

            var orderDirection = new OrderDirection();

            var actual = orderDirection.Apply(_filterStub.Object);

            actual.Should().Be(expected);
        }
    }
}
