using System;
using Nancy;
using Nancy.Responses;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.WebApi.Lib.Exceptions;

namespace UrlShortener.WebApi.Lib
{
    public class HandlerError
    {
        public static dynamic Config(NancyContext context, Exception exception)
        {
            var model = new
            {
                Errors = new[] { exception.Message }
            };

            var response = new JsonResponse(model, new DefaultJsonSerializer());

            if (exception is NotFoundException)
            {
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else if (exception is ValidationException)
            {
                response.StatusCode = HttpStatusCode.Conflict;
            }
            else
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}