namespace UrlShortener.Infrastructure.Data.Filter.Simple.Data
{
    public class Filter : Restful.Query.Filter.Filter
    {
        private readonly Restful.Query.Filter.Filter _filter;

        public virtual string Resource { get; set; }
        public override Restful.Query.Filter.Filters.Limit Limit => _filter.Limit;
        public override Restful.Query.Filter.Filters.Skip Skip => _filter.Skip;
        public override Restful.Query.Filter.Filters.Ordering.OrderBy OrderBy => _filter.OrderBy;
        public override Restful.Query.Filter.Filters.Condition.Where Where => _filter.Where;

        protected Filter()
        {

        }

        public Filter(Restful.Query.Filter.Filter filter)
        {
            _filter = filter;
        }
    }
}