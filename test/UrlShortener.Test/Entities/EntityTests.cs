using FluentAssertions;
using NUnit.Framework;
using UrlShortener.Entities;

namespace UrlShortener.Test.Entities
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
        public void MarkAsDeleted_DevePreencherDeleted()
        {
            _entity.MarkAsDeleted();

            _entity.Deleted.Should().BeTrue();
        }
    }
}
