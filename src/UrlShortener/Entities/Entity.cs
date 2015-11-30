using System;
using UrlShortener.Infrastructure;

namespace UrlShortener.Entities
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

        public virtual void MarkAsDeleted()
        {
            Deleted = true;
        }
    }
}