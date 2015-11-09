namespace UrlShortener.WebApi.Infrastructure.Data.Filter
{
    public interface ISkip<in TFilter>
        where TFilter : Restful.Query.Filter.Filter
    {
        int Apply(TFilter filter);
    }
}
