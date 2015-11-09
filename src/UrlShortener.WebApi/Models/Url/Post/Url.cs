namespace UrlShortener.WebApi.Models.Url.Post
{
    public class Url
    {
        public virtual string Address { get; set; }
        public virtual Account Account { get; set; }
    }
}
