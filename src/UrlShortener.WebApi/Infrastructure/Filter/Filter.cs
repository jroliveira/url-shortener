namespace UrlShortener.WebApi.Infrastructure.Filter
{
    public class Filter
    {
        public Limit Limit { get; set; }
        public Skip Skip { get; set; }
        public Order Order { get; set; }
        public string Resource { get; private set; }

        public bool HasOrder { get { return Order != null; } }

        public void SetResource(string resource)
        {
            Resource = resource;
        }

        public static implicit operator Filter(string query)
        {
            var filter = new Filter
            {
                Skip = query,
                Limit = query,
                Order = query
            };

            return filter;
        }
    }
}