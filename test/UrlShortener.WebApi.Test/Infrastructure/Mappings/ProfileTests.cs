using AutoMapper;
using NUnit.Framework;

namespace UrlShortener.WebApi.Test.Infrastructure.Mappings
{
    public class ProfileTests
    {
        protected IMappingEngine MappingEngine;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            AutoMapperConfig.RegisterProfiles();
            MappingEngine = Mapper.Engine;
        }
    }
}