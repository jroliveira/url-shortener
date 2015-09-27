using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;

namespace UrlShortener.WebApi.Infrastructure.Data.Queries.Url
{
    public class GetByShortened
    {
        public virtual Models.Url GetResult(string shortened)
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
                              .Where(
                                  db.Urls.Shortened == shortened)
                              .FirstOrDefault();

            if (data == null)
            {
                throw new NotFoundException();
            }

            var model = new Models.Url
            {
                Id = data.Id,
                Address = data.Address,
                Account = new Models.Account
                {
                    Id = data.AccountId
                }
            };

            return model;
        }
    }
}