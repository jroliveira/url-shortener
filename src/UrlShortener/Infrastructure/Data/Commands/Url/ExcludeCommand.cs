using Simple.Data;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Infrastructure.Data.Commands.Url
{
    public class ExcludeCommand
    {
        public virtual void Execute(int id)
        {
            var db = Database.OpenNamedConnection("db");

            Entities.Url entity = db.Urls.Get(id);

            if (entity == null || entity.Deleted)
            {
                throw new NotFoundException("Url {0} not found", id);
            }

            entity.MarkAsDeleted();

            db.Urls.Update(entity);
        }
    }
}