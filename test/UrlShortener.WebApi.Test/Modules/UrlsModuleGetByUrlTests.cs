using System;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.WebApi.Models.Url;
using UrlShortener.WebApi.Test.Lib;
using Url = UrlShortener.WebApi.Models.Url.Get.Url;

namespace UrlShortener.WebApi.Test.Modules
{
    [TestFixture]
    public class UrlsModuleGetByUrlTests : UrlsModuleTests
    {
        public override void SetUp()
        {
            GetByUrlMock
                .Setup(q => q.GetResult("8edd484c"))
                .Returns(new Url
                {
                    Id = 1,
                    Address = "http://jroliveira.net",
                    Account = new Account
                    {
                        Id = 1
                    }
                });
        }

        [Test]
        public void GetByUrl_HttpStatusCodeShouldBe200OK()
        {
            var response = Browser.Get("/urls/8edd484c", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void GetByUrl_ShouldCallGetByUrlQueryOnce()
        {
            Browser.Get("/urls/8edd484c", with =>
            {
                with.HttpRequest();
            });

            GetByUrlMock.Verify(q => q.GetResult("8edd484c"), Times.Once);
        }

        [Test]
        public void GetByUrl_ShouldReturnUrl()
        {
            var response = Browser.Get("/urls/8edd484c", with =>
            {
                with.HttpRequest();
            });

            var actual = "url-get-by-url-8edd484c.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetByUrl_WhenGenericExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            GetByUrlMock
                .Setup(q => q.GetResult("8edd484c"))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Get("/urls/8edd484c", with =>
            {
                with.HttpRequest();
            });

            var actual = "error.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetByUrl_WhenGenericExceptionIsThrown_HttpStatusCodeShouldBe500InternalServerError()
        {
            GetByUrlMock
                .Setup(q => q.GetResult("8edd484c"))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Get("/urls/8edd484c", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void GetByUrl_WhenResourceNotFound_ShouldReturnErrorJsonAsExpected()
        {
            GetByUrlMock
                .Setup(q => q.GetResult("8edd484c"))
                .Returns(default(Url));

            var response = Browser.Get("/urls/8edd484c", with =>
            {
                with.HttpRequest();
            });

            var actual = "url-get-by-url-8edd484c-not-found.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetByUrl_WhenResourceNotFound_HttpStatusCodeShouldBe404NotFound()
        {
            GetByUrlMock
                .Setup(q => q.GetResult("8edd484c"))
                .Returns(default(Url));

            var response = Browser.Get("/urls/8edd484c", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}