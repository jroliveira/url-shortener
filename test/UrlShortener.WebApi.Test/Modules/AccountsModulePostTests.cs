using System;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.WebApi.Models.Account.Post;
using UrlShortener.WebApi.Test.Lib;

namespace UrlShortener.WebApi.Test.Modules
{
    [TestFixture]
    public class AccountsModulePostTests : AccountsModuleTests
    {
        public override void SetUp()
        {
            CreateMock
                .Setup(c => c.Execute(It.IsAny<Account>()))
                .Returns(1);
        }

        [Test]
        public void Post_HttpStatusCodeShouldBe201Created()
        {
            var response = Browser.Post("/accounts", with =>
            {
                with.HttpRequest();
                with.Body("account-post.json".Load("request"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void Post_ShouldCallCreateCommandOnce()
        {
            Browser.Post("/accounts", with =>
            {
                with.HttpRequest();
                with.Header("Content-Type", "application/json");
                with.Body("account-post.json".Load("request"));
            });

            CreateMock.Verify(c => c.Execute(It.IsAny<Account>()), Times.Once);
        }

        [Test]
        public void Post_ShouldReturn1()
        {
            var response = Browser.Post("/accounts", with =>
            {
                with.HttpRequest();
                with.Body("account-post.json".Load("request"));
            });

            var actual = "account-post-1.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Post_WhenGenericExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            CreateMock
                .Setup(c => c.Execute(It.IsAny<Account>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Post("/accounts", with =>
            {
                with.HttpRequest();
                with.Body("account-post.json".Load("request"));
            });

            var actual = "error.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Post_WhenGenericExceptionIsThrown_HttpStatusCodeShouldBe500InternalServerError()
        {
            CreateMock
                .Setup(c => c.Execute(It.IsAny<Account>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Post("/accounts", with =>
            {
                with.HttpRequest();
                with.Body("account-post.json".Load("request"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}