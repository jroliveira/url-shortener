using System.Threading.Tasks;
using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Queries.Account
{
    public class GetById
    {
        public virtual async Task<Entities.Account> GetResult(int id)
        {
            var db = Database.Open();

            Entities.Account model = await db.Accounts.All()
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