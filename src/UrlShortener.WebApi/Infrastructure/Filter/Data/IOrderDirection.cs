namespace UrlShortener.WebApi.Infrastructure.Filter.Data
{
    public interface IOrderDirection<out T>
    {
        T Apply(Filter filter);
    }
}