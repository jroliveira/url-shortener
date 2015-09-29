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

        public GetAll(ISkip skip, ILimit limit)
        {
            _skip = skip;
            _limit = limit;
        }

        public virtual IEnumerable<Model.Get.Url> GetResult(Filter.Filter filter)
        {
            var db = Database.OpenNamedConnection("db");

            dynamic accounts;

            var data = db.Urls.All()
                              .Join(db.Accounts, out accounts)
                                  .On(db.Urls.AccountId == accounts.Id)
                              .Select(
                                  db.Urls.Id,
                                  db.Urls.Address,
                                  accounts.Id.As("AccountId"))
                              .Skip(_skip.Apply(filter))
                              .Take(_limit.Apply(filter));

            var model = new List<Model.Get.Url>();

            foreach (var item in data)
            {
                model.Add(new Model.Get.Url
                {
                    Id = item.Id,
                    Address = item.Address,
                    Account = new Model.Account
                    {
                        Id = item.AccountId
                    }
                });
            }

            if (model == null || !model.Any())
            {
                throw new NotFoundException();
            }

            return model;
        }
    }
}