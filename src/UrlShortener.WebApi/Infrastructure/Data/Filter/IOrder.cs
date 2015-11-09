namespace UrlShortener.WebApi.Infrastructure.Data.Filter
{
    public interface IOrder<in TFilter, out TReturn>
        where TFilter : Restful.Query.Filter.Filter
    {
        TReturn Apply(TFilter filter);
    }
}