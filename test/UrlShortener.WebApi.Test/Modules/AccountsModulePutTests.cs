using System;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.WebApi.Test.Lib;

namespace UrlShortener.WebApi.Test.Modules
{
    [TestFixture]
    public class AccountsModulePutTests : AccountsModuleTests
    {
        [Test]
        public void Put_HttpStatusCodeShouldBe204NoContent()
        {
            var response = Browser.Put("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public void Put_ShouldCallUpdateCommandOnce()
        {
            Browser.Put("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Body("account-put.json".Load("request"));
            });

            UpdateMock.Verify(c => c.Execute(1, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void Put_WhenGenericExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            UpdateMock
                .Setup(c => c.Execute(1, It.IsAny<object>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Put("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Body("account-put.json".Load("request"));
            });

            var actual = "error.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Put_WhenGenericExceptionIsThrown_HttpStatusCodeShouldBe500InternalServerError()
        {
            UpdateMock
                .Setup(c => c.Execute(1, It.IsAny<object>()))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Put("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Body("account-put.json".Load("request"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void Put_WhenResourceNotFound_ShouldReturnErrorJsonAsExpected()
        {
            UpdateMock
                .Setup(c => c.Execute(1, It.IsAny<object>()))
                .Throws(new NotFoundException("Account 1 not found"));

            var response = Browser.Put("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Body("account-put.json".Load("request"));
            });

            var actual = "account-put-1-not-found.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Put_WhenResourceNotFound_HttpStatusCodeShouldBe404NotFound()
        {
            UpdateMock
                .Setup(c => c.Execute(1, It.IsAny<object>()))
                .Throws(new NotFoundException("Account 1 not found"));

            var response = Browser.Put("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Body("account-put.json".Load("request"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}