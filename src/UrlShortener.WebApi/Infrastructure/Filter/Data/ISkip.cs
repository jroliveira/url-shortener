namespace UrlShortener.WebApi.Infrastructure.Filter.Data
{
    public interface ISkip
    {
        int Apply(Filter filter);
    }
}
