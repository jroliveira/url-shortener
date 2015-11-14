using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;

namespace UrlShortener.WebApi.Infrastructure.Data.Queries.Url
{
    public class GetByUrl
    {
        public virtual Models.Url.Get.Url GetResult(string shortened)
        {
            var db = Database.OpenNamedConnection("db");

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

            var model = Slapper.AutoMapper.MapDynamic<Models.Url.Get.Url>(data) as Models.Url.Get.Url;

            if (model == null)
            {
                throw new NotFoundException("Resource 'urls' with url shortener {0} could not be found", shortened);
            }

            return model;
        }
    }
}