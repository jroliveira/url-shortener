using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Account
{
    public class ExcludeCommand
    {
        public virtual void Execute(int id)
        {
            var db = Database.OpenNamedConnection("db");

            Entities.Account entity = db.Accounts.Get(id);

            if (entity == null || entity.Deleted)
            {
                throw new NotFoundException("Account {0} not found");
            }

            entity.MarkAsDeleted();

            db.Accounts.Update(entity);
        }
    }
}