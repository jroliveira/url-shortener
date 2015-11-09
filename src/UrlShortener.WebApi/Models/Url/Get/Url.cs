namespace UrlShortener.WebApi.Models.Url.Get
{
    public class Url
    {
        public virtual int Id { get; set; }
        public virtual string Address { get; set; }
        public virtual Account Account { get; set; }
    }
}
