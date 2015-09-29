using AutoMapper;
using Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib.Validators.Url;
using Model = UrlShortener.WebApi.Models.Url.Post;

namespace UrlShortener.WebApi.Infrastructure.Data.Commands.Url
{
    public class CreateCommand
    {
        private readonly UrlValidator _validator;
        private readonly IMappingEngine _mappingEngine;

        public CreateCommand(UrlValidator validator,
                             IMappingEngine mappingEngine)
        {
            _validator = validator;
            _mappingEngine = mappingEngine;
        }

        public virtual Entities.Url Execute(Model.Url model)
        {
            var validateResult = _validator.Validate(model);

            if (!validateResult.IsValid)
            {
                throw new UrlShortenerException(validateResult.Errors);
            }

            var entity = _mappingEngine.Map<Entities.Url>(model);

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