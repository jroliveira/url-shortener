using Simple.Data;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Infrastructure.Data.Commands.Account
{
    public class UpdateCommand
    {
        private readonly PartialUpdater _partialUpdater;

        public UpdateCommand(PartialUpdater partialUpdater)
        {
            _partialUpdater = partialUpdater;
        }

        public virtual void Execute(int id, dynamic changedModel)
        {
            var db = Database.Open();

            Entities.Account entity = db.Accounts.Get(id);

            if (entity == null || entity.Deleted)
            {
                throw new NotFoundException("Account {0} not found", id);
            }

            _partialUpdater.Apply(changedModel, entity);

            db.Accounts.Update(entity);
        }
    }
}