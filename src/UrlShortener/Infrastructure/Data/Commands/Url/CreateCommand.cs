using System.Threading.Tasks;
using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Commands.Url
{
    public class CreateCommand
    {
        public async virtual Task<Entities.Url> Execute(Entities.Url entity)
        {
            entity.Shorten();

            var db = Database.Open();

            var data = new
            {
                entity.Address,
                entity.Shortened,
                entity.CreationDate,
                entity.Deleted,
                AccountId = entity.Account.Id
            };

            var inserted = await db.Urls.Insert(data);

            entity.Id = inserted.Id;

            return entity;
        }
    }
}