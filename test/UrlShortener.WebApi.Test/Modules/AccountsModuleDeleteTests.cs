using System;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.Infrastructure.Exceptions;
using UrlShortener.WebApi.Test.Lib;
using UrlShortener.WebApi.Test.Lib.Extensions;

namespace UrlShortener.WebApi.Test.Modules
{
    [TestFixture]
    public class AccountsModuleDeleteTests : AccountsModuleTests
    {
        [Test]
        public void Delete_HttpStatusCodeShouldBe204NoContent()
        {
            var response = Browser.Delete("/accounts/1", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public void Delete_ShouldCallExcludeCommandOnce()
        {
            Browser.Delete("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            ExcludeMock.Verify(c => c.Execute(1), Times.Once);
        }

        [Test]
        public void Delete_WhenGenericExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            ExcludeMock
                .Setup(c => c.Execute(1))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Delete("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            var actual = "error.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Delete_WhenGenericExceptionIsThrown_HttpStatusCodeShouldBe500InternalServerError()
        {
            ExcludeMock
                .Setup(q => q.Execute(1))
                .Throws(new InvalidOperationException("Exception from testing"));

            var response = Browser.Delete("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void Delete_WhenResourceNotFound_ShouldReturnErrorJsonAsExpected()
        {
            ExcludeMock
                .Setup(q => q.Execute(1))
                .Throws(new NotFoundException("Account 1 not found"));

            var response = Browser.Delete("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            var actual = "account-delete-1-not-found.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Delete_WhenResourceNotFound_HttpStatusCodeShouldBe404NotFound()
        {
            ExcludeMock
                .Setup(q => q.Execute(1))
                .Throws(new NotFoundException("Account 1 not found"));

            var response = Browser.Delete("/accounts/1", with =>
            {
                with.HttpRequest();
            });

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}