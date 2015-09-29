using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib;
using Model = UrlShortener.WebApi.Models.Account.Put;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Account
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
            var db = Database.OpenNamedConnection("db");

            Entities.Account entity = db.Accounts.Get(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _partialUpdater.Apply(changedModel, entity);

            db.Accounts.Update(entity);
        }
    }
}