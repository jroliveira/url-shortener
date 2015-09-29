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

        public GetAll(ISkip skip, ILimit limit)
        {
            _skip = skip;
            _limit = limit;
        }

        public virtual IEnumerable<Model.Account> GetResult(Filter.Filter filter)
        {
            var db = Database.OpenNamedConnection("db");

            List<Model.Account> model = db.Accounts.All()
                                                   .Select(
                                                       db.Accounts.Id,
                                                       db.Accounts.Name,
                                                       db.Accounts.Email)
                                                   .Where(
                                                       db.Accounts.Deleted == false)
                                                   .Skip(_skip.Apply(filter))
                                                   .Take(_limit.Apply(filter))
                                                   .OrderBy(
                                                       db.Accounts.Name);

            if (model == null || !model.Any())
            {
                throw new NotFoundException();
            }

            return model;
        }
    }
}