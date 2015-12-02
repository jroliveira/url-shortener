using AutoMapper;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Security;
using Newtonsoft.Json;
using UrlShortener.Infrastructure;
using UrlShortener.Infrastructure.Data.Commands.Account;
using UrlShortener.Infrastructure.Data.Queries.Account;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib.Exceptions;
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

        private Negotiator All()
        {
            var entities = _getAll.GetResult(QueryStringFilter);

            if (entities == null)
            {
                throw new NotFoundException("Resource 'accounts' with filter passed could not be found");
            }

            var models = Mapper.Map<Paged<Models.Account.Get.Account>>(entities);

            return Negotiate.WithModel(models);
        }

        private Negotiator ById(int id)
        {
            var entity = _getById.GetResult(id);

            if (entity == null)
            {
                throw new NotFoundException("Resource 'accounts' with id {0} could not be found", id);
            }

            var model = Mapper.Map<Models.Account.Get.Account>(entity);

            return Negotiate.WithModel(model);
        }

        private Negotiator Create(Models.Account.Post.Account model)
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

            return
                Negotiate
                    .WithModel(response)
                    .WithStatusCode(HttpStatusCode.Created);
        }

        private Negotiator Update(int id, dynamic changedModel)
        {
            _update.Execute(id, changedModel);

            return Negotiate.WithStatusCode(HttpStatusCode.NoContent);
        }

        private Negotiator Exclude(int id)
        {
            _exclude.Execute(id);

            return Negotiate.WithStatusCode(HttpStatusCode.NoContent);
        }
    }
}