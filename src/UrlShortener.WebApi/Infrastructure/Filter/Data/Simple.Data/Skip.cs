namespace UrlShortener.WebApi.Infrastructure.Filter.Data.Simple.Data
{
    public class Skip : ISkip
    {
        private readonly ILimit _limit;

        public Skip(ILimit limit)
        {
            _limit = limit;
        }

        public int Apply(Filter filter)
        {
            if (filter.Skip == null)
            {
                return 0;
            }

            if (filter.Skip < 0)
            {
                return 0;
            }

            int skip = filter.Skip;

            if (filter.Limit != null)
            {
                skip = filter.Skip * _limit.Apply(filter);
            }

            return skip;
        }
    }
}
