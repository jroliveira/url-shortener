using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace UrlShortener.WebApi.Infrastructure.Exceptions
{
    public class ValidationException : Exception
    {
        private readonly IEnumerable<string> _errors;

        public IEnumerable<string> Errors { get { return _errors; } }

        public ValidationException(IEnumerable<ValidationFailure> validationFailures)
        {
            _errors = validationFailures.Select(c => c.ErrorMessage).ToArray();
        }
    }
}