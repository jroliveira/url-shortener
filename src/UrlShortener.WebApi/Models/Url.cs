namespace UrlShortener.WebApi.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Account Account { get; set; }
    }
}
