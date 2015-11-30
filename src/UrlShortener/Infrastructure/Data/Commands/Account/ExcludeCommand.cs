using Simple.Data;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Infrastructure.Data.Commands.Account
{
    public class ExcludeCommand
    {
        public virtual void Execute(int id)
        {
            var db = Database.Open();

            Entities.Account entity = db.Accounts.Get(id);

            if (entity == null || entity.Deleted)
            {
                throw new NotFoundException("Account {0} not found", id);
            }

            entity.MarkAsDeleted();

            db.Accounts.Update(entity);
        }
    }
}