using FluentAssertions;
using NUnit.Framework;
using Simple.Data;
using UrlShortener.Infrastructure.Data.Commands.Account;
using UrlShortener.Infrastructure.Security;

namespace UrlShortener.Test.Infrastructure.Data.Commands.Account
{
    [TestFixture]
    public class CreateCommandTests
    {
        private CreateCommand _command;
        private UrlShortener.Entities.Account _entity;

        [SetUp]
        public void SetUp()
        {
            _command = new CreateCommand();

            _entity = new UrlShortener.Entities.Account
            {
                Name = "Junior",
                Password = "123456",
                Email = "junolive@gmail.com"
            };

            var adapter = new InMemoryAdapter();
            adapter.SetKeyColumn("Accounts", "Id");
            adapter.SetAutoIncrementColumn("Accounts", "Id");

            Database.UseMockAdapter(adapter);
        }

        [Test]
        public void Execute_ShouldInsertAnAccount()
        {
            var insertedId = _command.Execute(_entity);

            var db = Database.Open();
            UrlShortener.Entities.Account actual = db.Accounts.Get(insertedId);

            var expected = _entity;
            expected.Id = 1;

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void Execute_GivenPassword_ShouldInsertAHashPassword()
        {
            var insertedId = _command.Execute(_entity);

            var db = Database.Open();
            UrlShortener.Entities.Account actual = db.Accounts.Get(insertedId);

            var hashAlgorithm = new Md5HashAlgorithm();
            var expected = hashAlgorithm.Hash("123456");

            actual.Password.Should().Be(expected);
        }
    }
}
