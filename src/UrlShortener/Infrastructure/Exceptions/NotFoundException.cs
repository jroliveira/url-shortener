﻿using System;

namespace UrlShortener.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {

        }

        public NotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {

        }
    }
}