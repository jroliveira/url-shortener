namespace UrlShortener.WebApi.Infrastructure.Security
{
    public interface IHashAlgorithm
    {
        string Hash(string text);
    }
}