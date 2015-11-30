using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Queries.Account
{
    public class GetByEmail
    {
        public virtual Entities.Account GetResult(string email)
        {
            var db = Database.Open();

            Entities.Account model = db.Accounts.All()
                                                .Select(
                                                    db.Accounts.Email,
                                                    db.Accounts.Password)
                                                .Where(
                                                    db.Accounts.Email == email
                                                    && db.Accounts.Deleted == false)
                                                .FirstOrDefault();

            return model;
        }
    }
}