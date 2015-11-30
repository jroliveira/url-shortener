using System.Collections.Generic;
using System.Linq;
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

        public GetAll(ISkip<Filter.Simple.Data.Filter> skip,
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

        public virtual IEnumerable<Entities.Account> GetResult(Filter.Simple.Data.Filter filter)
        {
            filter.Resource = "Accounts";

            DataStrategy strategy = Database.Open();

            var query = new SimpleQuery(strategy, filter.Resource);

            query = query.Skip(_skip.Apply(filter))
                         .Take(_limit.Apply(filter));

            if (filter.HasWhere)
            {
                query = query.Where(_where.Apply(filter));
            }

            if (filter.HasOrder)
            {
                query = query.OrderBy(_order.Apply(filter), _orderDirection.Apply(filter));
            }

            var model = query.ToList<Entities.Account>();

            if (model == null || !model.Any())
            {
                return null;
            }

            return model;
        }
    }
}