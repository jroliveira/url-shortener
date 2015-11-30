using System;
using FluentAssertions;
using NUnit.Framework;
using Simple.Data;
using UrlShortener.Infrastructure.Data.Commands.Account;
using UrlShortener.Infrastructure.Exceptions;

namespace UrlShortener.Test.Infrastructure.Data.Commands.Account
{
    [TestFixture]
    public class ExcludeCommandTests
    {
        private ExcludeCommand _command;

        [SetUp]
        public void SetUp()
        {
            _command = new ExcludeCommand();

            var adapter = new InMemoryAdapter();
            adapter.SetKeyColumn("Accounts", "Id");
            adapter.SetAutoIncrementColumn("Accounts", "Id");

            Database.UseMockAdapter(adapter);

            var db = Database.Open();
            db.Accounts.Insert(
                Name: "Junior",
                Password: "123456",
                Email: "junolive@gmail.com",
                CreationDate: DateTime.Now,
                Deleted: false
            );
        }

        [Test]
        public void Execute_ShouldMarkAccountAsDeleted()
        {
            _command.Execute(1);

            var db = Database.Open();
            UrlShortener.Entities.Account actual = db.Accounts.Get(1);

            actual.Deleted.Should().BeTrue();
        }

        [Test]
        public void Execute_GivenAnAccountNonexistent_ShouldThrowNotFoundException()
        {
            Action action = () => _command.Execute(2);

            action
                .ShouldThrow<NotFoundException>()
                .WithMessage("Account 2 not found");
        }

        [Test]
        public void Execute_GivenAnAccountDeleted_ShouldThrowNotFoundException()
        {
            var db = Database.Open();
            db.Accounts.Insert(
                Name: "Junior",
                Password: "123456",
                Email: "junolive@gmail.com",
                CreationDate: DateTime.Now,
                Deleted: true
            );

            Action action = () => _command.Execute(2);

            action
                .ShouldThrow<NotFoundException>()
                .WithMessage("Account 2 not found");
        }
    }
}
