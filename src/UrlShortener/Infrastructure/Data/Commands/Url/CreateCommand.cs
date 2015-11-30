using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Commands.Url
{
    public class CreateCommand
    {
        public virtual Entities.Url Execute(Entities.Url entity)
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

            var inserted = db.Urls.Insert(data);

            entity.Id = inserted.Id;

            return entity;
        }
    }
}