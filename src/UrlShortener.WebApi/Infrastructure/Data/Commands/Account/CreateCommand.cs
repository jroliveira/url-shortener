using AutoMapper;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib.Validators.Account.Post;
using Model = UrlShortener.WebApi.Models.Account.Post;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Account
{
    public class CreateCommand
    {
        private readonly AccountValidator _validator;
        private readonly IMappingEngine _mappingEngine;

        public CreateCommand(AccountValidator validator,
                             IMappingEngine mappingEngine)
        {
            _validator = validator;
            _mappingEngine = mappingEngine;
        }

        public virtual int Execute(Model.Account model)
        {
            var validateResult = _validator.Validate(model);

            if (!validateResult.IsValid)
            {
                throw new UrlShortenerException(validateResult.Errors);
            }

            var entity = _mappingEngine.Map<Entities.Account>(model);

            entity.HashPassword();

            var db = Database.OpenNamedConnection("db");

            var inserted = db.Accounts.Insert(entity);

            return inserted.Id;
        }
    }
}