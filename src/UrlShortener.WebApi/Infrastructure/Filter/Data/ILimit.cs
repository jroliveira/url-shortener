namespace UrlShortener.WebApi.Infrastructure.Filter.Data
{
    public interface ILimit
    {
        int Apply(Filter filter);
    }
}