namespace UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data
{
    public class Filter : Restful.Query.Filter.Filter
    {
        private readonly Restful.Query.Filter.Filter _filter;

        public virtual string Resource { get; set; }
        public override Restful.Query.Filter.Limit Limit { get { return _filter.Limit; } }
        public override Restful.Query.Filter.Skip Skip { get { return _filter.Skip; } }
        public override Restful.Query.Filter.Order.Order Order { get { return _filter.Order; } }
        public override Restful.Query.Filter.Where.Where Where { get { return _filter.Where; } }

        protected Filter()
        {

        }

        public Filter(Restful.Query.Filter.Filter filter)
        {
            _filter = filter;
        }
    }
}