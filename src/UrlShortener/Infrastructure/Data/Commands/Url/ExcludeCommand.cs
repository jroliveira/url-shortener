using System.Threading.Tasks;
using Simple.Data;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Infrastructure.Data.Commands.Url
{
    public class ExcludeCommand
    {
        public virtual async Task Execute(int id)
        {
            var db = Database.Open();

            Entities.Url entity = await db.Urls.Get(id);

            if (entity == null || entity.Deleted)
            {
                throw new NotFoundException("Url {0} not found", id);
            }

            entity.MarkAsDeleted();

            await db.Urls.Update(entity);
        }
    }
}