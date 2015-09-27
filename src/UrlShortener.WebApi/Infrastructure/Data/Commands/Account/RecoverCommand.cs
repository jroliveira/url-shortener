using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Account
{
    public class RecoverCommand
    {
        public virtual void Execute(int id)
        {
            var db = Database.OpenNamedConnection("db");

            Domain.Entities.Account entity = db.Accounts.Get(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.Recover();

            db.Accounts.Update(entity);
        }
    }
}