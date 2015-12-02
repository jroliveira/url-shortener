using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using UrlShortener.Entities;
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

            AccountValidatorMock
                .Setup(v => v.Validate(It.IsAny<Models.Account.Post.Account>()))
                .Returns(new ValidationResult());
        }

        [Test]
        public void Post_HttpStatusCodeShouldBe201Created()
        {
            var response = Browser.Post("/accounts", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
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
                with.Header("Accept", "application/json");
                with.Body("account-post.json".Load("request"));
            });

            var actual = "account-post-1.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Post_WhenValidationExceptionIsThrown_ShouldReturnErrorJsonAsExpected()
        {
            AccountValidatorMock
                .Setup(c => c.Validate(It.IsAny<Models.Account.Post.Account>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("Name", "Nome deve ser informado.")
                }));

            var response = Browser.Post("/accounts", with =>
            {
                with.HttpRequest();
                with.Body("account-post.json".Load("request"));
            });

            var actual = "account-post-conflict.json".Load("response");

            response.Body.AsString().Should().Be(actual);
        }

        [Test]
        public void Post_WhenValidationExceptionIsThrown_HttpStatusCodeShouldBe409Conflict()
        {
            AccountValidatorMock
                .Setup(c => c.Validate(It.IsAny<Models.Account.Post.Account>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("Name", "Nome deve ser informado.")
                }));

            var response = Browser.Post("/accounts", with =>
            {
                with.HttpRequest();
                with.Body("account-post.json".Load("request"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
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