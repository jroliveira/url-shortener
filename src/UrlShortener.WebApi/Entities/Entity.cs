using System;
using UrlShortener.WebApi.Infrastructure;

namespace UrlShortener.WebApi.Entities
{
    public class Entity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }

        public Entity()
        {
            CreationDate = Clock.Now();
        }

        public void Recover()
        {
            Deleted = false;
        }

        public void MarkAsDeleted()
        {
            Deleted = true;
        }
    }
}