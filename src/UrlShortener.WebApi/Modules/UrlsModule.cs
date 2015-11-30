using System.Collections.Generic;
using AutoMapper;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using UrlShortener.Infrastructure.Data.Commands.Url;
using UrlShortener.Infrastructure.Data.Queries.Url;
using UrlShortener.Infrastructure.Exceptions;
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
            : base("urls")
        {
            _getAll = getAll;
            _getByShortened = getByShortened;
            _create = create;
            _exclude = exclude;
            _validator = validator;

            this.RequiresAuthentication();

            Get["/"] = _ => All();
            Get["/{url}"] = parameters => ByUrl(parameters.url);
            Post["/"] = _ => Create(this.Bind<Models.Url.Post.Url>());
            Delete["/{id}"] = parameters => Exclude(parameters.id);
        }

        private Response All()
        {
            var entities = _getAll.GetResult(QueryStringFilter);

            if (entities == null)
            {
                throw new NotFoundException("Resource 'urls' with filter passed could not be found");
            }

            var models = Mapper.Map<IEnumerable<Models.Url.Get.Url>>(entities);

            return Response.AsJson(models);
        }

        private Response ByUrl(string url)
        {
            var entity = _getByShortened.GetResult(url);

            if (entity == null)
            {
                throw new NotFoundException("Resource 'urls' with url {0} could not be found", url);
            }

            var model = Mapper.Map<Models.Url.Get.Url>(entity);

            return Response.AsJson(model);
        }

        private Response Create(Models.Url.Post.Url model)
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

            return Response.AsJson(response, HttpStatusCode.Created);
        }

        private Response Exclude(int id)
        {
            _exclude.Execute(id);

            return HttpStatusCode.NoContent;
        }
    }
}
