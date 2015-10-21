using UrlShortener.WebApi.Infrastructure;

namespace UrlShortener.WebApi.Entities
{
    public class Url : Entity<int>
    {
        private readonly Shortener _shortener;

        public string Address { get; set; }
        public Account Account { get; set; }
        public string Shortened { get; private set; }

        internal Url(Shortener shortener)
        {
            _shortener = shortener;
        }

        public Url()
            : this(new Shortener())
        {

        }

        public void Shorten()
        {
            Shortened = _shortener.Shorten(Address);
        }
    }
}
