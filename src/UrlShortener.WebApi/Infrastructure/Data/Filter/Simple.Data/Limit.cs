namespace UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data
{
    public class Limit : ILimit<Filter>
    {
        public int Apply(Filter filter)
        {
            if (filter.Limit == null)
            {
                return 100;
            }

            if (filter.Limit < 1)
            {
                return 100;
            }

            return filter.Limit;
        }
    }
}