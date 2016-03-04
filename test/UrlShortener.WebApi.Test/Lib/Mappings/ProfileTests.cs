using AutoMapper;
using NUnit.Framework;
using UrlShortener.WebApi.Lib.Mappings;

namespace UrlShortener.WebApi.Test.Lib.Mappings
{
    public class ProfileTests
    {
        protected IMapper Mapper;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountProfile>();
                cfg.AddProfile<UrlProfile>();
                cfg.AddProfile<PagedProfile>();
            });

            Mapper = autoMapperConfig.CreateMapper();
        }
    }
}