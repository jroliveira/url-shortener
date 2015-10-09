namespace UrlShortener.WebApi.Infrastructure.Filter
{
    public class Filter
    {
        public string Resource { get; private set; }
        public Limit Limit { get; set; }
        public Skip Skip { get; set; }
        public Order.Order Order { get; set; }
        public Where.Where Where { get; set; }

        public bool HasOrder { get { return Order != null; } }
        public bool HasWhere { get { return Where != null; } }

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
                Order = query,
                Where = query
            };

            return filter;
        }
    }
}