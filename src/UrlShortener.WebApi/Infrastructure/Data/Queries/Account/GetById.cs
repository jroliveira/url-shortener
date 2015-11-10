using Simple.Data;
using Model = UrlShortener.WebApi.Models.Account.Get;

namespace UrlShortener.WebApi.Infrastructure.Data.Queries.Account
{
    public class GetById
    {
        public virtual Model.Account GetResult(int id)
        {
            var db = Database.OpenNamedConnection("db");

            Model.Account model = db.Accounts.All()
                                             .Select(
                                                 db.Accounts.Id,
                                                 db.Accounts.Name,
                                                 db.Accounts.Email)
                                             .Where(
                                                 db.Accounts.Id == id
                                                 && db.Accounts.Deleted == false)
                                             .FirstOrDefault();

            return model;
        }
    }
}