namespace UrlShortener.WebApi.Infrastructure.Filter.Data
{
    public interface IOrder<out T>
    {
        T Apply(Filter filter);
    }
}