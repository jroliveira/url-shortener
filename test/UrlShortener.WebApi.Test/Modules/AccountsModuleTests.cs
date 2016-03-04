using AutoMapper;
using Moq;
using Nancy.Hal.Configuration;
using Nancy.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using UrlShortener.Infrastructure.Data.Commands.Account;
using UrlShortener.Infrastructure.Data.Queries.Account;
using UrlShortener.WebApi.Lib;
using UrlShortener.WebApi.Lib.Authentication;
using UrlShortener.WebApi.Lib.Hal;
using UrlShortener.WebApi.Lib.Mappings;
using UrlShortener.WebApi.Lib.Validators;
using UrlShortener.WebApi.Modules;

namespace UrlShortener.WebApi.Test.Modules
{
    public class AccountsModuleTests
    {
        protected Mock<GetAll> GetAllMock;
        protected Mock<GetById> GetByIdMock;
        protected Mock<CreateCommand> CreateMock;
        protected Mock<UpdateCommand> UpdateMock;
        protected Mock<ExcludeCommand> ExcludeMock;
        protected Mock<AccountValidator> AccountValidatorMock;

        protected Browser Browser;

        [SetUp]
        public void SetUpBase()
        {
            GetAllMock = new Mock<GetAll>();
            GetByIdMock = new Mock<GetById>();
            CreateMock = new Mock<CreateCommand>();
            UpdateMock = new Mock<UpdateCommand>();
            ExcludeMock = new Mock<ExcludeCommand>();
            AccountValidatorMock = new Mock<AccountValidator>();

            var configurable = new ConfigurableBootstrapper(with =>
            {
                with.Module<AccountsModule>();

                with.ApplicationStartup((container, pipelines) =>
                {
                    var autoMapperConfig = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<AccountProfile>();
                        cfg.AddProfile<PagedProfile>();
                    });

                    container.Register((factory, overloads) => autoMapperConfig.CreateMapper());

                    container.Register<JsonSerializer, CustomJsonSerializer>();
                    container.Register<IProvideHalTypeConfiguration>(Hypermedia.Configuration());
                });

                with.RequestStartup((container, pipelines, context) =>
                {
                    pipelines.OnError.AddItemToEndOfPipeline(HandlerError.Config);
                    context.CurrentUser = new UserIdentity
                    {
                        Claims = new[] { "admin" },
                        UserName = "admin"
                    };
                });

                with.Dependency(GetAllMock.Object);
                with.Dependency(GetByIdMock.Object);
                with.Dependency(CreateMock.Object);
                with.Dependency(UpdateMock.Object);
                with.Dependency(ExcludeMock.Object);
                with.Dependency(AccountValidatorMock.Object);
            });

            Browser = new Browser(configurable);

            SetUp();
        }

        public virtual void SetUp()
        {

        }
    }
}