using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Filter.Order;

namespace UrlShortener.WebApi.Infrastructure.Filter.Data.Simple.Data
{
    public class OrderDirection : IOrderDirection<OrderByDirection>
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