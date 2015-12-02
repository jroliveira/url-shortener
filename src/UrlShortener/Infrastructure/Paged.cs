using System;
using System.Collections.Generic;

namespace UrlShortener.Infrastructure
{
    public class Paged<T>
    {
        public virtual ICollection<T> Data { get; set; }
        public virtual int Skip { get; set; }
        public virtual int Limit { get; set; }
        public virtual int Count { get { return Data.Count; } }
        public virtual long Pages { get { return Limit == 0 ? 1 : (long)Math.Ceiling((double)Count / Limit); } }

        public void Add(T item)
        {
            Data.Add(item);
        }
    }
}