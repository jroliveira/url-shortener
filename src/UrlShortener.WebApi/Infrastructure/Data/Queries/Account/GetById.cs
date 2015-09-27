using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;

namespace UrlShortener.WebApi.Infrastructure.Data.Queries.Account
{
    public class GetById
    {
        public virtual Models.Account GetResult(int id)
        {
            var db = Database.OpenNamedConnection("db");

            Models.Account model = db.Accounts.All()
                                              .Select(
                                                  db.Accounts.Id,
                                                  db.Accounts.Name,
                                                  db.Accounts.Email)
                                              .Where(
                                                  db.Accounts.Id == id)
                                              .FirstOrDefault();

            if (model == null)
            {
                throw new NotFoundException();
            }

            return model;
        }
    }
}