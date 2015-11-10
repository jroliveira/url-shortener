using FluentAssertions;
using NUnit.Framework;
using UrlShortener.WebApi.Entities;

namespace UrlShortener.WebApi.Test.Entities
{
    [TestFixture]
    public class UrlTests
    {
        private Url _url;

        [SetUp]
        public void SetUp()
        {
            _url = new Url();
        }

        [TestCase("http://jroliveira.net", "8edd484c")]
        [TestCase("http://www.google.com.br", "ec1f31c7")]
        [TestCase("http://www.google.com", "d9c085ad")]
        [TestCase("http://www.github.com", "7a3d78e5")]
        [TestCase("http://www.facebook.com", "ed28cf7e")]
        public void Shorten_DadoAddressDeveRetornarShortened(string address, string shortened)
        {
            _url.Address = address;

            _url.Shorten();

            _url.Shortened.Should().Be(shortened);
        }
    }
}