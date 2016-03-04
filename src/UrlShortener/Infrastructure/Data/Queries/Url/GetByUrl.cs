using System.Threading.Tasks;
using Simple.Data;
using Slapper;

namespace UrlShortener.Infrastructure.Data.Queries.Url
{
    public class GetByUrl
    {
        public virtual async Task<Entities.Url> GetResult(string shortened)
        {
            var db = Database.Open();

            dynamic accounts;

            var data = await db.Urls.All()
                                    .Join(db.Accounts, out accounts)
                                        .On(db.Urls.AccountId == accounts.Id)
                                    .Select(
                                        db.Urls.Id,
                                        db.Urls.Address,
                                        accounts.Id.As("Account_Id"))
                                    .Where(
                                        db.Urls.Shortened == shortened)
                                    .FirstOrDefault();

            var entity = AutoMapper.MapDynamic<Entities.Url>(data) as Entities.Url;

            return entity;
        }
    }
}