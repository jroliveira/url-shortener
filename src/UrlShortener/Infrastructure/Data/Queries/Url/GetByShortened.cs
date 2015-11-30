using Simple.Data;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Infrastructure.Data.Queries.Url
{
    public class GetByUrl
    {
        public virtual Entities.Url GetResult(string shortened)
        {
            var db = Database.Open();

            dynamic accounts;

            var data = db.Urls.All()
                              .Join(db.Accounts, out accounts)
                                  .On(db.Urls.AccountId == accounts.Id)
                              .Select(
                                  db.Urls.Id,
                                  db.Urls.Address,
                                  accounts.Id.As("Account_Id"))
                              .Where(
                                  db.Urls.Shortened == shortened)
                              .FirstOrDefault();

            var model = Slapper.AutoMapper.MapDynamic<Entities.Url>(data) as Entities.Url;

            if (model == null)
            {
                throw new NotFoundException("Resource 'urls' with url shortener {0} could not be found", shortened);
            }

            return model;
        }
    }
}