using FluentAssertions;
using NUnit.Framework;
using UrlShortener.WebApi.Entities;

namespace UrlShortener.WebApi.Test.Entities
{
    [TestFixture]
    public class EntityTests
    {
        private Entity<int> _entity;

        [SetUp]
        public void SetUp()
        {
            _entity = new Entity<int>();
        }

        [Test]
        public void Recover_DevePreencherDeleted()
        {
            _entity.Recover();

            _entity.Deleted.Should().BeFalse();
        }

        [Test]
        public void MarkAsDeleted_DevePreencherDeleted()
        {
            _entity.MarkAsDeleted();

            _entity.Deleted.Should().BeTrue();
        }
    }
}
