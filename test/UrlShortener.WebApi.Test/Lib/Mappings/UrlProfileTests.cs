using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.Entities;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Test.Lib.Mappings
{
    [TestFixture]
    public class UrlProfileTests : ProfileTests
    {
        private Mock<Url> _entityStub;

        [SetUp]
        public void SetUp()
        {
            Clock.Now = () => new DateTime(2015, 11, 05);

            _entityStub = new Mock<Url>();
            _entityStub.Setup(e => e.CreationDate).Returns(new DateTime(2015, 11, 05));
            _entityStub.Setup(e => e.Deleted).Returns(false);
            _entityStub.Setup(e => e.Account.CreationDate).Returns(new DateTime(2015, 11, 05));
        }

        [Test]
        public void Map_GivenModelToEntity_ShouldMapProperly()
        {
            var modelStub = new Mock<Models.Url.Post.Url>();
            modelStub.Setup(m => m.Address).Returns("http://jroliveira.net");
            modelStub.Setup(m => m.Account.Id).Returns(1);

            var entity = Mapper.Map<Url>(modelStub.Object);

            _entityStub.Setup(e => e.Id).Returns(0);
            _entityStub.Setup(e => e.Address).Returns("http://jroliveira.net");
            _entityStub.Setup(e => e.Account.Id).Returns(1);

            entity.ShouldBeEquivalentTo(_entityStub.Object);
        }
    }
}