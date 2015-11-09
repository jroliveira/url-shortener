using Restful.Query.Filter.Order;
using Simple.Data;

namespace UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data
{
    public class OrderDirection : IOrderDirection<Filter, OrderByDirection>
    {
        public OrderByDirection Apply(Filter filter)
        {
            if (filter.Order.Sorts == Sorts.Desc)
            {
                return OrderByDirection.Descending;
            }

            return OrderByDirection.Ascending;
        }
    }
}