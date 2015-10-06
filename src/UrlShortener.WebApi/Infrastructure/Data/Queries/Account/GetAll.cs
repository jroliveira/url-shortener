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
        private readonly IOrder<ObjectReference> _order;

        public GetAll(ISkip skip, ILimit limit, IOrder<ObjectReference> order)
        {
            _skip = skip;
            _limit = limit;
            _order = order;
        }

        public virtual IEnumerable<Model.Account> GetResult(Filter.Filter filter)
        {
            filter.SetResource("Accounts");

            DataStrategy strategy = Database.OpenNamedConnection("db");

            var query = new SimpleQuery(strategy, filter.Resource);

            query = query.Skip(_skip.Apply(filter))
                         .Take(_limit.Apply(filter));

            if (filter.HasOrder)
            {
                query = query.OrderBy(_order.Apply(filter), OrderByDirection.Ascending);
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