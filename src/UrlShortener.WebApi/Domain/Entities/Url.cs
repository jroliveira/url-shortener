﻿using UrlShortener.WebApi.Infrastructure;

namespace UrlShortener.WebApi.Domain.Entities
{
    public class Url : Entity<int>
    {
        private readonly Shortener _shortener;

        public string Address { get; set; }
        public Account Account { get; set; }
        public string Shortened { get; private set; }

        public Url()
        {
            _shortener = new Shortener();
        }

        public void Shorten()
        {
            Shortened = _shortener.Shorten(Address);
        }
    }
}
