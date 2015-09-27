using AutoMapper;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib;
using UrlShortener.WebApi.Lib.Validators;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Account
{
    public class UpdateCommand
    {
        private readonly AccountValidator _validator;
        private readonly IMappingEngine _mappingEngine;
        private readonly PartialUpdater _partialUpdater;

        public UpdateCommand(AccountValidator validator,
                             IMappingEngine mappingEngine,
                             PartialUpdater partialUpdater)
        {
            _validator = validator;
            _mappingEngine = mappingEngine;
            _partialUpdater = partialUpdater;
        }

        public virtual void Execute(int id, dynamic changedModel)
        {
            var db = Database.OpenNamedConnection("db");

            Domain.Entities.Account entity = db.Accounts.Get(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var currentModel = _mappingEngine.Map<Models.Account>(entity);

            _partialUpdater.Apply(changedModel, currentModel);

            var validateResult = _validator.Validate(currentModel);

            if (!validateResult.IsValid)
            {
                throw new UrlShortenerException(validateResult.Errors);
            }

            entity = _mappingEngine.Map<Domain.Entities.Account>(currentModel);

            db.Accounts.Update(entity);
        }
    }
}