using System.Linq;
using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Filter.Simple.Data
{
    public class OrderDirection : IOrderDirection<Filter, OrderByDirection>
    {
        public OrderByDirection Apply(Filter filter)
        {
            if (filter.OrderBy.First().Direction == Restful.Query.Filter.Filters.Ordering.OrderByDirection.Descending)
            {
                return OrderByDirection.Descending;
            }

            return OrderByDirection.Ascending;
        }
    }
}