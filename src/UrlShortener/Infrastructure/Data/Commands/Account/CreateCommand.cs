using System.Threading.Tasks;
using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Commands.Account
{
    public class CreateCommand
    {
        public virtual async Task<int> Execute(Entities.Account entity)
        {
            entity.HashPassword();

            var db = Database.Open();

            var inserted = await db.Accounts.Insert(entity);

            return inserted.Id;
        }
    }
}