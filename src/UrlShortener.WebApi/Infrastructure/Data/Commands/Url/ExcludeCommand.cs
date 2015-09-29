using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Url
{
    public class ExcludeCommand
    {
        public virtual void Execute(int id)
        {
            var db = Database.OpenNamedConnection("db");

            Entities.Url entity = db.Urls.Get(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            entity.MarkAsDeleted();

            db.Urls.Update(entity);
        }
    }
}