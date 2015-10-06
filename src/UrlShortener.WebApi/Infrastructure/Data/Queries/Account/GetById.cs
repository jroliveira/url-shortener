using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
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

            if (model == null)
            {
                throw new NotFoundException("Resource 'accounts' with id {0} could not be found", id);
            }

            return model;
        }
    }
}