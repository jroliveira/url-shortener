using System;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.WebApi.Test.Lib;
using Url = UrlShortener.WebApi.Models.Url.Post.Url;

namespace UrlShortener.WebApi.Test.Modules
{
    [TestFixture]
    public class UrlsModulePostTests : UrlsModuleTests
    {
        public override void SetUp()
        {
            var entityStub = new Mock<WebApi.Entities.Url>();
            entityStub.Setup(e => e.Shortened).Returns("8edd484c");
            entityStub.Setup(e => e.Id).Returns(1);

            CreateMock
                .Setup(c => c.Execute(It.IsAny<Url>()))
                .Returns(entityStub.Object);
        }

        [Test]
        public void Post_HttpStatusCodeShouldBe201Created()
        {
            var response = Browser.Post("/urls", with =>
            {
                with.HttpRequest();
                with.Body("url-post.json".Load("request"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void Post_ShouldCallCreateCommandOnce()
        {
            Browser.Post("/urls", with =>
            {
                with.HttpRequest();
                with.Header("Content-Type", "application/json");
                with.Body("account-post.json".Load("request"));
            });

            CreateMock.Verify(c => c.Execute(It.IsAny<Url>()), Times.Once);
        }

        [Test]
        public void Post_ShouldReturnId1AndUrl8edd484c()
        {
            var response = Browser.Post("/urls", with =>
            {
                with.HttpRequest();
                with.Body("url-post.json".Load("request"));
            });

            var actual = "url-post-8edd484c.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Post_WhenGenericExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            CreateMock
                .Setup(c => c.Execute(It.IsAny<Url>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Post("/urls", with =>
            {
                with.HttpRequest();
                with.Body("url-post.json".Load("request"));
            });

            var actual = "error.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Post_WhenGenericExceptionIsThrown_HttpStatusCodeShouldBe500InternalServerError()
        {
            CreateMock
                .Setup(c => c.Execute(It.IsAny<Url>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Post("/urls", with =>
            {
                with.HttpRequest();
                with.Body("url-post.json".Load("request"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}