using System.Linq;
using System.Threading.Tasks;
using Simple.Data;
using Slapper;
using UrlShortener.Infrastructure.Data.Filter;

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

        public GetAll(
            ISkip<Filter.Simple.Data.Filter> skip,
            ILimit<Filter.Simple.Data.Filter> limit,
            IOrder<Filter.Simple.Data.Filter, ObjectReference> order)
        {
            _skip = skip;
            _limit = limit;
            _order = order;
        }

        public virtual async Task<Paged<Entities.Url>> GetResult(Filter.Simple.Data.Filter filter, int? accountId = null)
        {
            filter.Resource = "Urls";

            DataStrategy strategy = Database.Open();

            var query = new SimpleQuery(strategy, filter.Resource);

            var limit = _limit.Apply(filter);
            var skip = _skip.Apply(filter);

            dynamic accounts;

            query = query.Join(ObjectReference.FromString("Accounts"), JoinType.Inner, out accounts)
                             .On(accounts.Id == new ObjectReference("AccountId", ObjectReference.FromString("Urls")))
                         .Select(
                             new ObjectReference("Id", ObjectReference.FromString("Urls")),
                             new ObjectReference("Address", ObjectReference.FromString("Urls")),
                             new ObjectReference("Id", ObjectReference.FromString("Accounts")).As("Account_Id"))
                         .Skip(_skip.Apply(filter))
                         .Take(_limit.Apply(filter));

            if (accountId.HasValue)
            {
                var leftOperand = new ObjectReference("AccountId", ObjectReference.FromString("Urls"));

                query.Where(new SimpleExpression(leftOperand, accountId, SimpleExpressionType.Equal));
            }

            if (filter.HasOrdering)
            {
                query = query.OrderBy(_order.Apply(filter), OrderByDirection.Ascending);
            }

            var data = await query.ToList<dynamic>();

            var entities = AutoMapper.MapDynamic<Entities.Url>(data).ToList();

            if (!entities.Any())
            {
                return null;
            }

            return new Paged<Entities.Url>
            {
                Limit = limit,
                Skip = skip,
                Data = entities
            };
        }
    }
}