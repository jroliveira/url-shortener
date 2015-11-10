using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using UrlShortener.WebApi.Infrastructure.Data.Commands.Account;
using UrlShortener.WebApi.Infrastructure.Data.Queries.Account;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Models.Account.Post;

namespace UrlShortener.WebApi.Modules
{
    public class AccountsModule : BaseModule
    {
        private readonly GetAll _getAll;
        private readonly GetById _getById;
        private readonly CreateCommand _create;
        private readonly UpdateCommand _update;
        private readonly ExcludeCommand _exclude;

        public AccountsModule(GetAll getAll,
                              GetById getById,
                              CreateCommand create,
                              UpdateCommand update,
                              ExcludeCommand exclude)
            : base("accounts")
        {
            _getAll = getAll;
            _getById = getById;
            _create = create;
            _update = update;
            _exclude = exclude;

            Get["/"] = _ => HandleError(() => All());
            Get["/{id}"] = parameters => HandleError(() => ById(parameters.id));
            Post["/"] = _ => HandleError(() => Create(this.Bind<Account>()));
            Put["/{id}"] = parameters => HandleError(() => Update(parameters.id, JsonConvert.DeserializeObject(Request.Body.AsString())));
            Delete["/{id}"] = parameters => HandleError(() => Exclude(parameters.id));
        }

        private Response All()
        {
            var filter = GetFilter();

            var model = _getAll.GetResult(filter);

            if (model == null)
            {
                throw new NotFoundException("Resource 'accounts' with filter passed could not be found");
            }

            return Response.AsJson(model);
        }

        private Response ById(int id)
        {
            var model = _getById.GetResult(id);

            if (model == null)
            {
                throw new NotFoundException("Resource 'accounts' with id {0} could not be found", id);
            }

            return Response.AsJson(model);
        }

        private Response Update(int id, dynamic changedModel)
        {
            _update.Execute(id, changedModel);

            return HttpStatusCode.NoContent;
        }

        private Response Create(Account model)
        {
            var insertedId = _create.Execute(model);

            var response = new
            {
                Id = insertedId
            };

            return Response.AsJson(response, HttpStatusCode.Created);
        }

        private Response Exclude(int id)
        {
            _exclude.Execute(id);

            return HttpStatusCode.NoContent;
        }
    }
}