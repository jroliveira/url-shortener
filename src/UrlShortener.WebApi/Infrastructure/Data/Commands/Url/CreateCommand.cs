using AutoMapper;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Infrastructure.Validators;
using Model = UrlShortener.WebApi.Models.Url.Post;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Url
{
    public class CreateCommand
    {
        private readonly UrlValidator _validator;

        public CreateCommand(UrlValidator validator)
        {
            _validator = validator;
        }

        public virtual Entities.Url Execute(Model.Url model)
        {
            var validateResult = _validator.Validate(model);

            if (!validateResult.IsValid)
            {
                throw new UrlShortenerException(validateResult.Errors);
            }

            var entity = Mapper.Map<Entities.Url>(model);

            entity.Shorten();

            var db = Database.OpenNamedConnection("db");

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