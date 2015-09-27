using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using UrlShortener.WebApi.Infrastructure.Extensions;

namespace UrlShortener.WebApi.Infrastructure.Exceptions
{
    public class UrlShortenerException : Exception
    {
        private readonly IEnumerable<string> _errors;

        public IEnumerable<string> Errors { get { return _errors; } }

        public UrlShortenerException(string[] errors)
            : base(errors.ToInlineMessage())
        {
            _errors = errors;
        }

        public UrlShortenerException(IEnumerable<ValidationFailure> validationFailures)
            : this(validationFailures.Select(c => c.ErrorMessage).ToArray())
        {

        }
    }
}