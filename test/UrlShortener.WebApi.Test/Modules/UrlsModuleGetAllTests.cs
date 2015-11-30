using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.Entities;
using UrlShortener.Infrastructure.Data.Filter.Simple.Data;
using UrlShortener.WebApi.Test.Lib;
using Url = UrlShortener.Entities.Url;

namespace UrlShortener.WebApi.Test.Modules
{
    [TestFixture]
    public class UrlsModuleGetAllTests : UrlsModuleTests
    {
        public override void SetUp()
        {
            GetAllMock
                .Setup(q => q.GetResult(It.IsAny<Filter>()))
                .Returns(new List<Url>
                {
                    new Url
                    {
                        Id = 1, 
                        Address = "http://jroliveira.net",
                        Account = new Account
                        {
                            Id = 1
                        }
                    }
                });
        }

        [Test]
        public void GetAll_HttpStatusCodeShouldBe200OK()
        {
            var response = Browser.Get("/urls", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void GetAll_ShouldCallGetAllQueryOnce()
        {
            Browser.Get("/urls", with =>
            {
                with.HttpRequest();
            });

            GetAllMock.Verify(q => q.GetResult(It.IsAny<Filter>()), Times.Once);
        }

        [Test]
        public void GetAll_ShouldReturnUrls()
        {
            var response = Browser.Get("/urls", with =>
            {
                with.HttpRequest();
            });

            var actual = "url-get-all.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetAll_WhenGenericExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            GetAllMock
                .Setup(q => q.GetResult(It.IsAny<Filter>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Get("/urls", with =>
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

            var response = Browser.Get("/urls", with =>
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
                .Returns(default(List<Url>));

            var response = Browser.Get("/urls", with =>
            {
                with.HttpRequest();
            });

            var actual = "url-get-all-not-found.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void GetAll_WhenResourceNotFound_HttpStatusCodeShouldBe404NotFound()
        {
            GetAllMock
                .Setup(q => q.GetResult(It.IsAny<Filter>()))
                .Returns(default(List<Url>));

            var response = Browser.Get("/urls", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}