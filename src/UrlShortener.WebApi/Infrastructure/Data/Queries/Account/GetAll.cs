using System.Collections.Generic;
using System.Linq;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Infrastructure.Filter.Data;
using Model = UrlShortener.WebApi.Models.Account.Get;

namespace UrlShortener.WebApi.Infrastructure.Data.Queries.Account
{
    public class GetAll
    {
        private readonly ISkip _skip;
        private readonly ILimit _limit;
        private readonly IWhere<SimpleExpression> _where;
        private readonly IOrder<ObjectReference> _order;
        private readonly IOrderDirection<OrderByDirection> _orderDirection;
        
        public GetAll(ISkip skip,
                      ILimit limit,
                      IWhere<SimpleExpression> where,
                      IOrder<ObjectReference> order,
                      IOrderDirection<OrderByDirection> orderDirection)
        {
            _skip = skip;
            _limit = limit;
            _where = where;
            _order = order;
            _orderDirection = orderDirection;
        }

        public virtual IEnumerable<Model.Account> GetResult(Filter.Filter filter)
        {
            filter.SetResource("Accounts");

            DataStrategy strategy = Database.OpenNamedConnection("db");

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

            var model = query.ToList<Model.Account>();

            if (model == null || !model.Any())
            {
                throw new NotFoundException("Resource 'accounts' with filter passed could not be found");
            }

            return model;
        }
    }
}