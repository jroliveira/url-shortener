using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.WebApi.Entities;
using UrlShortener.WebApi.Infrastructure;

namespace UrlShortener.WebApi.Test.Infrastructure.Mappings
{
    [TestFixture]
    public class AccountProfileTests : ProfileTests
    {
        private Mock<Account> _entityStub;

        [SetUp]
        public void SetUp()
        {
            Clock.Now = () => new DateTime(2015, 11, 05);

            _entityStub = new Mock<Account>();
            _entityStub.Setup(e => e.CreationDate).Returns(new DateTime(2015, 11, 05));
            _entityStub.Setup(e => e.Deleted).Returns(false);
        }

        [Test]
        public void Map_GivenAccountPostModelToEntity_ShouldMapProperly()
        {
            var modelStub = new Mock<Models.Account.Post.Account>();
            modelStub.Setup(m => m.Name).Returns("Junior Oliveira");
            modelStub.Setup(m => m.Email).Returns("junolive@gmail.com");
            modelStub.Setup(m => m.Password).Returns("123456");
            modelStub.Setup(m => m.ConfirmPassword).Returns("123456");

            var entity = MappingEngine.Map<Account>(modelStub.Object);

            _entityStub.Setup(e => e.Name).Returns("Junior Oliveira");
            _entityStub.Setup(e => e.Email).Returns("junolive@gmail.com");
            _entityStub.Setup(e => e.Password).Returns("123456");

            entity.ShouldBeEquivalentTo(_entityStub.Object);
        }

        [Test]
        public void Map_GivenUrlAccountModelToEntity_ShouldMapProperly()
        {
            var modelStub = new Mock<Models.Url.Account>();
            modelStub.Setup(m => m.Id).Returns(1);

            var entity = MappingEngine.Map<Account>(modelStub.Object);

            _entityStub.Setup(e => e.Id).Returns(1);

            entity.ShouldBeEquivalentTo(_entityStub.Object);
        }
    }
}
