using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Account
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

        public virtual void Execute(int id, dynamic changedModel)
        {
            var db = Database.OpenNamedConnection("db");

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