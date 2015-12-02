using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.Entities;
using UrlShortener.Infrastructure;
using UrlShortener.Infrastructure.Data.Filter.Simple.Data;
using UrlShortener.WebApi.Test.Lib;

namespace UrlShortener.WebApi.Test.Modules
{
    [TestFixture]
    public class AccountsModuleGetAllTests : AccountsModuleTests
    {
        public override void SetUp()
        {
            GetAllMock
                .Setup(q => q.GetResult(It.IsAny<Filter>()))
                .Returns(new Paged<Account>
                {
                    Data = new List<Account>
                    {
                        new Account
                        {
                            Id = 1,
                            Name = "Junior",
                            Email = "junolive@gmail.com"
                        }
                    }
                });
        }

        [Test]
        public void GetAll_HttpStatusCodeShouldBe200OK()
        {
            var response = Browser.Get("/accounts", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void GetAll_ShouldCallGetAllQueryOnce()
        {
            Browser.Get("/accounts", with =>
            {
                with.HttpRequest();
            });

            GetAllMock.Verify(q => q.GetResult(It.IsAny<Filter>()), Times.Once);
        }

        [Test]
        public void GetAll_ShouldReturnAccounts()
        {
            var response = Browser.Get("/accounts", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            var actual = "account-get-all.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetAll_WhenGenericExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            GetAllMock
                .Setup(q => q.GetResult(It.IsAny<Filter>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Get("/accounts", with =>
            {
                with.HttpRequest();
            });

            var actual = "error.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetAll_WhenGenericExceptionIsThrown_HttpStatusCodeShouldBe500InternalServerError()
        {
            GetAllMock
                .Setup(q => q.GetResult(It.IsAny<Filter>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Get("/accounts", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void GetAll_WhenResourceNotFound_ShouldReturnErrorJsonAsExpected()
        {
            GetAllMock
                .Setup(q => q.GetResult(It.IsAny<Filter>()))
                .Returns(default(Paged<Account>));

            var response = Browser.Get("/accounts", with =>
            {
                with.HttpRequest();
            });

            var actual = "account-get-all-not-found.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetAll_WhenResourceNotFound_HttpStatusCodeShouldBe404NotFound()
        {
            GetAllMock
                .Setup(q => q.GetResult(It.IsAny<Filter>()))
                .Returns(default(Paged<Account>));

            var response = Browser.Get("/accounts", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
