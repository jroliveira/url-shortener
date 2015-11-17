using Moq;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.WebApi.Infrastructure;
using UrlShortener.WebApi.Infrastructure.Data.Commands.Url;
using UrlShortener.WebApi.Infrastructure.Data.Queries.Url;
using UrlShortener.WebApi.Modules;

namespace UrlShortener.WebApi.Test.Modules
{
    public class UrlsModuleTests
    {
        protected Mock<GetAll> GetAllMock;
        protected Mock<GetByUrl> GetByUrlMock;
        protected Mock<CreateCommand> CreateMock;
        protected Mock<ExcludeCommand> ExcludeMock;

        protected Browser Browser;

        [SetUp]
        public void SetUpBase()
        {
            GetAllMock = new Mock<GetAll>();
            GetByUrlMock = new Mock<GetByUrl>();
            CreateMock = new Mock<CreateCommand>();
            ExcludeMock = new Mock<ExcludeCommand>();

            var configurable = new ConfigurableBootstrapper(with =>
            {
                with.Module<UrlsModule>();
                with.RequestStartup((container, pipelines, context) =>
                {
                    pipelines.OnError.AddItemToEndOfPipeline(HandlerError.Config);
                });

                with.Dependency(GetAllMock.Object);
                with.Dependency(GetByUrlMock.Object);
                with.Dependency(CreateMock.Object);
                with.Dependency(ExcludeMock.Object);
            });

            Browser = new Browser(configurable);

            SetUp();
        }

        public virtual void SetUp()
        {

        }
    }
}
