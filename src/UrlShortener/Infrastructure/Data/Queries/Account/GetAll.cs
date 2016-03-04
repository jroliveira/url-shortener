using System.Linq;
using System.Threading.Tasks;
using Simple.Data;
using UrlShortener.Infrastructure.Data.Filter;

namespace UrlShortener.Infrastructure.Data.Queries.Account
{
    public class GetAll
    {
        private readonly ISkip<Filter.Simple.Data.Filter> _skip;
        private readonly ILimit<Filter.Simple.Data.Filter> _limit;
        private readonly IWhere<Filter.Simple.Data.Filter, SimpleExpression> _where;
        private readonly IOrder<Filter.Simple.Data.Filter, ObjectReference> _order;
        private readonly IOrderDirection<Filter.Simple.Data.Filter, OrderByDirection> _orderDirection;

        protected GetAll()
        {

        }

        public GetAll(
            ISkip<Filter.Simple.Data.Filter> skip,
            ILimit<Filter.Simple.Data.Filter> limit,
            IWhere<Filter.Simple.Data.Filter, SimpleExpression> where,
            IOrder<Filter.Simple.Data.Filter, ObjectReference> order,
            IOrderDirection<Filter.Simple.Data.Filter, OrderByDirection> orderDirection)
        {
            _skip = skip;
            _limit = limit;
            _where = where;
            _order = order;
            _orderDirection = orderDirection;
        }

        public virtual async Task<Paged<Entities.Account>> GetResult(Filter.Simple.Data.Filter filter)
        {
            filter.Resource = "Accounts";

            DataStrategy strategy = Database.Open();

            var query = new SimpleQuery(strategy, filter.Resource);

            var limit = _limit.Apply(filter);
            var skip = _skip.Apply(filter);

            query = query.Skip(skip)
                         .Take(limit);

            if (filter.HasWhere)
            {
                query = query.Where(_where.Apply(filter));
            }

            if (filter.HasOrder)
            {
                query = query.OrderBy(_order.Apply(filter), _orderDirection.Apply(filter));
            }

            var entities = await query.ToList<Entities.Account>();

            if (entities == null || !entities.Any())
            {
                return null;
            }

            return new Paged<Entities.Account>
            {
                Limit = limit,
                Skip = skip,
                Data = entities
            };
        }
    }
}