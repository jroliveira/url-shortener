using NUnit.Framework;
using Infra = UrlShortener.WebApi.Infrastructure.Filter;

namespace Restful.Query.Filter.Test
{
    [TestFixture]
    public class FilterTests
    {
        private const string Query = "?filter[skip]=1&filter[limit]=2&filter[order]=id%20desc&filter[where][id][gt]=2";

        [Test]
        public void Parse_DadaQueryComSkip_SkipNaoPodeSerNull()
        {
            Infra.Filter filter = Query;

            filter.Skip.Should().NotBeNull();
        }

        [Test]
        public void Parse_DadaQueryComLimit_LimitNaoPodeSerNull()
        {
            Infra.Filter filter = Query;

            filter.Limit.Should().NotBeNull();
        }

        [Test]
        public void Parse_DadaQueryComOrder_OrderNaoPodeSerNull()
        {
            Infra.Filter filter = Query;

            filter.Order.Should().NotBeNull();
            filter.HasOrder.Should().BeTrue();
        }

        [Test]
        public void Parse_DadaQueryComWhere_WhereNaoPodeSerNull()
        {
            Infra.Filter filter = Query;

            filter.Where.Should().NotBeNull();
            filter.HasWhere.Should().BeTrue();
        }
    }
}
