namespace UrlShortener.WebApi.Infrastructure.Filter.Data
{
    public interface IWhere<out T>
    {
        T Apply(Filter filter);
    }
}