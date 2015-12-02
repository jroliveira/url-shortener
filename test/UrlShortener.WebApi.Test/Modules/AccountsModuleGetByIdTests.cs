using System;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.Entities;
using UrlShortener.WebApi.Test.Lib;

namespace UrlShortener.WebApi.Test.Modules
{
    [TestFixture]
    public class AccountsModuleGetByIdTests : AccountsModuleTests
    {
        public override void SetUp()
        {
            GetByIdMock
                .Setup(q => q.GetResult(1))
                .Returns(new Account
                {
                    Id = 1,
                    Name = "Junior",
                    Email = "junolive@gmail.com"
                });
        }

        [Test]
        public void GetById_HttpStatusCodeShouldBe200OK()
        {
            var response = Browser.Get("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void GetById_ShouldCallGetByIdQueryOnce()
        {
            Browser.Get("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            GetByIdMock.Verify(q => q.GetResult(1), Times.Once);
        }

        [Test]
        public void GetById_ShouldReturnAccount()
        {
            var response = Browser.Get("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            var actual = "account-get-by-id-1.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetById_WhenGenericExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            GetByIdMock
                .Setup(q => q.GetResult(1))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Get("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            var actual = "error.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetById_WhenGenericExceptionIsThrown_HttpStatusCodeShouldBe500InternalServerError()
        {
            GetByIdMock
                .Setup(q => q.GetResult(1))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Get("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void GetById_WhenResourceNotFound_ShouldReturnErrorJsonAsExpected()
        {
            GetByIdMock
                .Setup(q => q.GetResult(1))
                .Returns(default(Account));

            var response = Browser.Get("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            var actual = "account-get-by-id-1-not-found.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetById_WhenResourceNotFound_HttpStatusCodeShouldBe404NotFound()
        {
            GetByIdMock
                .Setup(q => q.GetResult(1))
                .Returns(default(Account));

            var response = Browser.Get("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}