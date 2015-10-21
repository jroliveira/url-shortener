using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.WebApi.Entities;
using UrlShortener.WebApi.Infrastructure;

namespace UrlShortener.WebApi.Test.Entities
{
    [TestFixture]
    public class UrlTests
    {
        private Url _url;
        private Mock<Shortener> _shortenerMock;

        [SetUp]
        public void SetUp()
        {
            _shortenerMock = new Mock<Shortener>();

            _url = new Url(_shortenerMock.Object);
        }

        [Test]
        public void Shorten_DeveChamarShortenerUmaVez()
        {
            _url.Shorten();

            _shortenerMock.Verify(m => m.Shorten(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Shorten_DevePreencherPropriedadeShortened()
        {
            _shortenerMock
                .Setup(m => m.Shorten(It.IsAny<string>()))
                .Returns("123");

            _url.Shorten();

            _url.Shortened.Should().Be("123");
        }
    }
}