using System.Threading.Tasks;
using Simple.Data;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Infrastructure.Data.Commands.Account
{
    public class UpdateCommand
    {
        private readonly PartialUpdater _partialUpdater;

        protected UpdateCommand()
        {

        }

        public UpdateCommand(PartialUpdater partialUpdater)
        {
            _partialUpdater = partialUpdater;
        }

        public virtual async Task Execute(int id, dynamic changedModel)
        {
            var db = Database.Open();

            Entities.Account entity = await db.Accounts.Get(id);

            if (entity == null || entity.Deleted)
            {
                throw new NotFoundException("Account {0} not found", id);
            }

            _partialUpdater.Apply(changedModel, entity);

            await db.Accounts.Update(entity);
        }
    }
}