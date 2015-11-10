using System;
using Nancy;

namespace UrlShortener.WebApi.Infrastructure.Extensions
{
    public static class ResponseExtensions
    {
        public static Response AsException(this IResponseFormatter response, Exception exception, HttpStatusCode statusCode)
        {
            var model = new
            {
                errors = new[] { exception.Message }
            };

            return response.AsJson(model, statusCode);
        }
    }
}