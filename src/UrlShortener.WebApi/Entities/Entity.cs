using System;
using UrlShortener.WebApi.Infrastructure;

namespace UrlShortener.WebApi.Entities
{
    public class Entity<TId>
    {
        public virtual TId Id { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual bool Deleted { get; set; }

        public Entity()
        {
            CreationDate = Clock.Now();
        }

        public virtual void Recover()
        {
            Deleted = false;
        }

        public virtual void MarkAsDeleted()
        {
            Deleted = true;
        }
    }
}