namespace UrlShortener.WebApi.Infrastructure.Filter
{
    public class Filter
    {
        public virtual string Resource { get; set; }
        public virtual Limit Limit { get; private set; }
        public virtual Skip Skip { get; private set; }
        public virtual Order.Order Order { get; private set; }
        public Where.Where Where { get; private set; }

        public bool HasOrder { get { return Order != null; } }
        public bool HasWhere { get { return Where != null; } }

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