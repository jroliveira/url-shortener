using System;
using Nancy;
using UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data;
using UrlShortener.WebApi.Infrastructure.Exceptions;
using UrlShortener.WebApi.Infrastructure.Extensions;

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
            catch (NotFoundException exception)
            {
                return Response.AsException(exception, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return Response.AsException(exception, HttpStatusCode.InternalServerError);
            }
        }

        protected Filter GetFilter()
        {
            var filter = Request.Url.Query;

            return new Filter(filter);
        }
    }
}