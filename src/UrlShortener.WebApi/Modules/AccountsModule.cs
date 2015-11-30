using System.Collections.Generic;
using AutoMapper;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Security;
using Newtonsoft.Json;
using UrlShortener.Infrastructure.Data.Commands.Account;
using UrlShortener.Infrastructure.Data.Queries.Account;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib.Validators;

namespace UrlShortener.WebApi.Modules
{
    public class AccountsModule : BaseModule
    {
        private readonly GetAll _getAll;
        private readonly GetById _getById;
        private readonly CreateCommand _create;
        private readonly UpdateCommand _update;
        private readonly ExcludeCommand _exclude;
        private readonly AccountValidator _validator;

        public AccountsModule(GetAll getAll,
                              GetById getById,
                              CreateCommand create,
                              UpdateCommand update,
                              ExcludeCommand exclude,
                              AccountValidator validator)
            : base("accounts")
        {
            _getAll = getAll;
            _getById = getById;
            _create = create;
            _update = update;
            _exclude = exclude;
            _validator = validator;

            this.RequiresAuthentication();

            Get["/"] = _ => All();
            Get["/{id}"] = parameters => ById(parameters.id);
            Post["/"] = _ => Create(this.Bind<Models.Account.Post.Account>());
            Put["/{id}"] = parameters => Update(parameters.id, JsonConvert.DeserializeObject(Request.Body.AsString()));
            Delete["/{id}"] = parameters => Exclude(parameters.id);
        }

        private Response All()
        {
            var entities = _getAll.GetResult(QueryStringFilter);

            if (entities == null)
            {
                throw new NotFoundException("Resource 'accounts' with filter passed could not be found");
            }

            var models = Mapper.Map<IEnumerable<Models.Account.Get.Account>>(entities);

            return Response.AsJson(models);
        }

        private Response ById(int id)
        {
            var entity = _getById.GetResult(id);

            if (entity == null)
            {
                throw new NotFoundException("Resource 'accounts' with id {0} could not be found", id);
            }

            var model = Mapper.Map<Models.Account.Get.Account>(entity);

            return Response.AsJson(model);
        }

        private Response Create(Models.Account.Post.Account model)
        {
            var validateResult = _validator.Validate(model);

            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var entity = Mapper.Map<Entities.Account>(model);

            var insertedId = _create.Execute(entity);

            var response = new
            {
                Id = insertedId
            };

            return Response.AsJson(response, HttpStatusCode.Created);
        }

        private Response Update(int id, dynamic changedModel)
        {
            _update.Execute(id, changedModel);

            return HttpStatusCode.NoContent;
        }

        private Response Exclude(int id)
        {
            _exclude.Execute(id);

            return HttpStatusCode.NoContent;
        }
    }
}