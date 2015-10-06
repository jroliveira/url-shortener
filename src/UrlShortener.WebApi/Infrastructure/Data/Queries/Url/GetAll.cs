using System.Collections.Generic;
using System.Linq;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Infrastructure.Filter.Data;
using Model = UrlShortener.WebApi.Models.Url;

namespace UrlShortener.WebApi.Infrastructure.Data.Queries.Url
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

        public virtual IEnumerable<Model.Get.Url> GetResult(Filter.Filter filter)
        {
            filter.SetResource("Urls");

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

            var model = Slapper.AutoMapper.MapDynamic<Model.Get.Url>(data)
                                          .ToList();

            if (model == null || !model.Any())
            {
                throw new NotFoundException();
            }

            return model;
        }
    }
}