using Nancy;
using Nancy.ModelBinding;
using UrlShortener.WebApi.Infrastructure.Data.Commands.Url;
using UrlShortener.WebApi.Infrastructure.Data.Queries.Url;
using Model = UrlShortener.WebApi.Models.Url;

namespace UrlShortener.WebApi.Modules
{
    public class UrlsModule : BaseModule
    {
        private readonly GetAll _getAll;
        private readonly GetByShortened _getByShortened;
        private readonly CreateCommand _create;
        private readonly ExcludeCommand _exclude;

        public UrlsModule(GetAll getAll,
                          GetByShortened getByShortened,
                          CreateCommand create,
                          ExcludeCommand exclude)
            : base("urls")
        {
            _getAll = getAll;
            _getByShortened = getByShortened;
            _create = create;
            _exclude = exclude;

            Get["/"] = _ => HandleError(() => All());
            Get["/{shortened}"] = parameters => HandleError(() => ByShortened(parameters.shortened));
            Post["/"] = _ => HandleError(() => Create(this.Bind<Model.Post.Url>()));
            Delete["/{id}"] = parameters => HandleError(() => Exclude(parameters.id));
        }

        private Response All()
        {
            var filter = GetFilter();

            var model = _getAll.GetResult(filter);

            return Response.AsJson(model);
        }

        private Response ByShortened(string shortened)
        {
            var model = _getByShortened.GetResult(shortened);

            return Response.AsJson(model);
        }

        private Response Create(Model.Post.Url model)
        {
            var entity = _create.Execute(model);

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
