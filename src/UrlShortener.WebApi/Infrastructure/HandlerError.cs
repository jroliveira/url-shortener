using System;
using Nancy;
using Nancy.Responses;
using UrlShortener.WebApi.Infrastructure.Exceptions;

namespace UrlShortener.WebApi.Infrastructure
{
    public class HandlerError
    {
        public static dynamic Config(NancyContext context, Exception exception)
        {
            var model = new
            {
                errors = new[] { exception.Message }
            };

            var response = new JsonResponse(model, new DefaultJsonSerializer());

            if (exception is NotFoundException)
            {
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}