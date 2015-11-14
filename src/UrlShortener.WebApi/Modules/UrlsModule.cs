﻿using Nancy;
using Nancy.ModelBinding;
using UrlShortener.WebApi.Infrastructure.Data.Commands.Url;
using UrlShortener.WebApi.Infrastructure.Data.Queries.Url;
using Url = UrlShortener.WebApi.Models.Url.Post.Url;

namespace UrlShortener.WebApi.Modules
{
    public class UrlsModule : BaseModule
    {
        private readonly GetAll _getAll;
        private readonly GetByUrl _getByShortened;
        private readonly CreateCommand _create;
        private readonly ExcludeCommand _exclude;

        public UrlsModule(GetAll getAll,
                          GetByUrl getByShortened,
                          CreateCommand create,
                          ExcludeCommand exclude)
            : base("urls")
        {
            _getAll = getAll;
            _getByShortened = getByShortened;
            _create = create;
            _exclude = exclude;

            Get["/"] = _ => All();
            Get["/{url}"] = parameters => ByShortened(parameters.url);
            Post["/"] = _ => Create(this.Bind<Url>());
            Delete["/{id}"] = parameters => Exclude(parameters.id);
        }

        private Response All()
        {
            var model = _getAll.GetResult(QueryStringFilter);

            return Response.AsJson(model);
        }

        private Response ByShortened(string url)
        {
            var model = _getByShortened.GetResult(url);

            return Response.AsJson(model);
        }

        private Response Create(Url model)
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
