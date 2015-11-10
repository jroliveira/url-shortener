using Moq;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.WebApi.Infrastructure.Data.Commands.Account;
using UrlShortener.WebApi.Infrastructure.Data.Queries.Account;
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

        protected Browser Browser;

        [SetUp]
        public void SetUpBase()
        {
            GetAllMock = new Mock<GetAll>();
            GetByIdMock = new Mock<GetById>();
            CreateMock = new Mock<CreateCommand>();
            UpdateMock = new Mock<UpdateCommand>();
            ExcludeMock = new Mock<ExcludeCommand>();

            var configurable = new ConfigurableBootstrapper(with =>
            {
                with.Module<AccountsModule>();
                with.Dependency(GetAllMock.Object);
                with.Dependency(GetByIdMock.Object);
                with.Dependency(CreateMock.Object);
                with.Dependency(UpdateMock.Object);
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