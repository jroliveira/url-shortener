using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Queries.Account
{
    public class GetById
    {
        public virtual Entities.Account GetResult(int id)
        {
            var db = Database.Open();

            Entities.Account model = db.Accounts.All()
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