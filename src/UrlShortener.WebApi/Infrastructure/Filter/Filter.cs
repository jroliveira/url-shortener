namespace UrlShortener.WebApi.Infrastructure.Filter
{
    public class Filter
    {
        public Limit Limit { get; set; }
        public Skip Skip { get; set; }

        public static implicit operator Filter(string query)
        {
            var filter = new Filter
            {
                Skip = query,
                Limit = query,
            };

            return filter;
        }
    }
}