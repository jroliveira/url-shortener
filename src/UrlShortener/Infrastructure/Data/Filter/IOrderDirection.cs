namespace UrlShortener.Infrastructure.Data.Filter
{
    public interface IOrderDirection<in TFilter, out TReturn>
        where TFilter : Restful.Query.Filter.Filter
    {
        TReturn Apply(TFilter filter);
    }
}