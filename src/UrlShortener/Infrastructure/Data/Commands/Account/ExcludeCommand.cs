using System.Threading.Tasks;
using Simple.Data;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Infrastructure.Data.Commands.Account
{
    public class ExcludeCommand
    {
        public virtual async Task Execute(int id)
        {
            var db = Database.Open();

            Entities.Account entity = await db.Accounts.Get(id);

            if (entity == null || entity.Deleted)
            {
                throw new NotFoundException("Account {0} not found", id);
            }

            entity.MarkAsDeleted();

            await db.Accounts.Update(entity);
        }
    }
}