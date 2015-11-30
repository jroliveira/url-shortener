using System.Linq;
using Restful.Query.Filter.Order;
using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Filter.Simple.Data
{
    public class OrderDirection : IOrderDirection<Filter, OrderByDirection>
    {
        public OrderByDirection Apply(Filter filter)
        {
            if (filter.Order.Fields.First().Sorts == Sorts.Desc)
            {
                return OrderByDirection.Descending;
            }

            return OrderByDirection.Ascending;
        }
    }
}