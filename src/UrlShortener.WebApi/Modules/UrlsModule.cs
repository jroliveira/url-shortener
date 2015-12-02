using AutoMapper;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Security;
using UrlShortener.Infrastructure;
using UrlShortener.Infrastructure.Data.Commands.Url;
using UrlShortener.Infrastructure.Data.Queries.Url;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib.Exceptions;
using UrlShortener.WebApi.Lib.Validators;

namespace UrlShortener.WebApi.Modules
{
    public class UrlsModule : BaseModule
    {
        private readonly GetAll _getAll;
        private readonly GetByUrl _getByShortened;
        private readonly CreateCommand _create;
        private readonly ExcludeCommand _exclude;
        private readonly UrlValidator _validator;

        public UrlsModule(GetAll getAll,
                          GetByUrl getByShortened,
                          CreateCommand create,
                          ExcludeCommand exclude,
                          UrlValidator validator)
        {
            _getAll = getAll;
            _getByShortened = getByShortened;
            _create = create;
            _exclude = exclude;
            _validator = validator;

            this.RequiresAuthentication();

            Get["urls/"] = _ => All();
            Get["accounts/{id}/urls/"] = parameters => All(parameters.id);
            Get["urls/{url}"] = parameters => ByUrl(parameters.url);
            Post["urls/"] = _ => Create(this.Bind<Models.Url.Post.Url>());
            Delete["urls/{id}"] = parameters => Exclude(parameters.id);
        }

        private Negotiator All(int? accountId = null)
        {
            var entities = _getAll.GetResult(QueryStringFilter, accountId);

            if (entities == null)
            {
                throw new NotFoundException("Resource 'urls' with filter passed could not be found");
            }

            var models = Mapper.Map<Paged<Models.Url.Get.Url>>(entities);

            return Negotiate.WithModel(models);
        }

        private Negotiator ByUrl(string url)
        {
            var entity = _getByShortened.GetResult(url);

            if (entity == null)
            {
                throw new NotFoundException("Resource 'urls' with url {0} could not be found", url);
            }

            var model = Mapper.Map<Models.Url.Get.Url>(entity);

            return Negotiate.WithModel(model);
        }

        private Negotiator Create(Models.Url.Post.Url model)
        {
            var validateResult = _validator.Validate(model);

            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }

            var entity = Mapper.Map<Entities.Url>(model);

            entity = _create.Execute(entity);

            var response = new
            {
                entity.Id,
                Address = string.Format("{0}/{1}", Request.Url, entity.Shortened)
            };

            return
                Negotiate
                    .WithModel(response)
                    .WithStatusCode(HttpStatusCode.Created);
        }

        private Negotiator Exclude(int id)
        {
            _exclude.Execute(id);

            return Negotiate.WithStatusCode(HttpStatusCode.NoContent);
        }
    }
}
