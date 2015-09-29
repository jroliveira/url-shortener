using System;
using Nancy;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Infrastructure.Filter;

namespace UrlShortener.WebApi.Modules
{
    public abstract class BaseModule : NancyModule
    {
        protected BaseModule(string modulePath)
            : base(modulePath)
        {

        }

        protected dynamic HandleError(Func<dynamic> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (NotFoundException)
            {
                return HttpStatusCode.NotFound;
            }
            catch (Exception exception)
            {
                var model = new
                {
                    erros = new[] { exception.Message }
                };

                return Response.AsJson(model, HttpStatusCode.InternalServerError);
            }
        }

        protected Filter GetFilter()
        {
            var filter = Request.Url.Query;

            return filter;
        }
    }
}