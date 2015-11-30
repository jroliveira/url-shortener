using System.Collections.Generic;
using System.Linq;
using Simple.Data;
using UrlShortener.Infrastructure.Data.Filter;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Infrastructure.Data.Queries.Url
{
    public class GetAll
    {
        private readonly ISkip<Filter.Simple.Data.Filter> _skip;
        private readonly ILimit<Filter.Simple.Data.Filter> _limit;
        private readonly IOrder<Filter.Simple.Data.Filter, ObjectReference> _order;

        protected GetAll()
        {

        }

        public GetAll(ISkip<Filter.Simple.Data.Filter> skip,
                      ILimit<Filter.Simple.Data.Filter> limit,
                      IOrder<Filter.Simple.Data.Filter, ObjectReference> order)
        {
            _skip = skip;
            _limit = limit;
            _order = order;
        }

        public virtual IEnumerable<Entities.Url> GetResult(Filter.Simple.Data.Filter filter)
        {
            filter.Resource = "Urls";

            DataStrategy strategy = Database.OpenNamedConnection("db");

            var query = new SimpleQuery(strategy, filter.Resource);

            dynamic accounts;

            query = query.Join(ObjectReference.FromString("Accounts"), JoinType.Inner, out accounts)
                             .On(accounts.Id == new ObjectReference("AccountId", ObjectReference.FromString("Urls")))
                         .Select(
                             new ObjectReference("Id", ObjectReference.FromString("Urls")),
                             new ObjectReference("Address", ObjectReference.FromString("Urls")),
                             new ObjectReference("Id", ObjectReference.FromString("Accounts")).As("Account_Id"))
                         .Skip(_skip.Apply(filter))
                         .Take(_limit.Apply(filter));

            if (filter.HasOrder)
            {
                query = query.OrderBy(_order.Apply(filter), OrderByDirection.Ascending);
            }

            var data = query.ToList<dynamic>();

            var model = Slapper.AutoMapper.MapDynamic<Entities.Url>(data)
                                          .ToList();

            if (model == null || !model.Any())
            {
                throw new NotFoundException("Resource 'urls' with filter passed could not be found");
            }

            return model;
        }
    }
}