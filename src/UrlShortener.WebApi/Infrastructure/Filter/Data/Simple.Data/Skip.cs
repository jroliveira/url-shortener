namespace UrlShortener.WebApi.Infrastructure.Filter.Data.Simple.Data
{
    public class Skip : ISkip
    {
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

            return filter.Skip;
        }
    }
}
