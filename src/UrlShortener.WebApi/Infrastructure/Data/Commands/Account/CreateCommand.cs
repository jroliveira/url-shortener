using AutoMapper;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Infrastructure.Validators;
using Model = UrlShortener.WebApi.Models.Account.Post;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Account
{
    public class CreateCommand
    {
        private readonly AccountValidator _validator;

        protected CreateCommand()
        {

        }

        public CreateCommand(AccountValidator validator)
        {
            _validator = validator;
        }

        public virtual int Execute(Model.Account model)
        {
            var validateResult = _validator.Validate(model);

            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var entity = Mapper.Map<Entities.Account>(model);

            entity.HashPassword();

            var db = Database.OpenNamedConnection("db");

            var inserted = db.Accounts.Insert(entity);

            return inserted.Id;
        }
    }
}