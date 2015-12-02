using Nancy.Hal.Configuration;

namespace UrlShortener.WebApi.Lib.Hal
{
    public class Hypermedia
    {
        public static HalConfiguration Configuration()
        {
            var config = new HalConfiguration();

            Account.Configuration.Setup(config);
            Url.Configuration.Setup(config);

            return config;
        }
    }
}