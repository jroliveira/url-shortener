using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.WebApi.Infrastructure.Validators;
using UrlShortener.WebApi.Models.Account.Post;

namespace UrlShortener.WebApi.Test.Infrastructure.Validators
{
    [TestFixture]
    public class AccountValidatorTests
    {
        private AccountValidator _validator;
        private Mock<Account> _modelStub;

        [SetUp]
        public void SetUp()
        {
            _validator = new AccountValidator();

            _modelStub = new Mock<Account>();
            _modelStub.Setup(m => m.Name).Returns("Junior Oliveira");
            _modelStub.Setup(m => m.Email).Returns("junolive@gmail.com");
            _modelStub.Setup(m => m.Password).Returns("123456");
            _modelStub.Setup(m => m.ConfirmPassword).Returns("123456");
        }

        [Test]
        public void Validade_GivenNameIsNull_ShouldReturnFalse()
        {
            _modelStub.Setup(m => m.Name).Returns(default(string));

            var result = _validator.Validate(_modelStub.Object);

            result
                .Errors
                .Any(c => c.ErrorMessage.Equals("Nome deve ser informado."))
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validade_GivenNameIsNotNull_ShouldReturnTrue()
        {
            _modelStub.Setup(m => m.Name).Returns("Junior Oliveira");

            var result = _validator.Validate(_modelStub.Object);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validade_GivenEmailIsNull_ShouldReturnFalse()
        {
            _modelStub.Setup(m => m.Email).Returns(default(string));

            var result = _validator.Validate(_modelStub.Object);

            result
                .Errors
                .Any(c => c.ErrorMessage.Equals("E-mail deve ser informado."))
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validade_GivenEmailIsNotNull_ShouldReturnTrue()
        {
            _modelStub.Setup(m => m.Email).Returns("junolive@gmail.com");

            var result = _validator.Validate(_modelStub.Object);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validade_GivenConfirmPasswordIsNotEqualToPassword_ShouldReturnFalse()
        {
            _modelStub.Setup(m => m.ConfirmPassword).Returns("123456");
            _modelStub.Setup(m => m.Password).Returns("654321");

            var result = _validator.Validate(_modelStub.Object);

            result
                .Errors
                .Any(c => c.ErrorMessage.Equals("Confirmação de senha deve ser igual a senha."))
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validade_GivenConfirmPasswordIsEqualToPassword_ShouldReturnTrue()
        {
            _modelStub.Setup(m => m.ConfirmPassword).Returns("123456");
            _modelStub.Setup(m => m.Password).Returns("123456");

            var result = _validator.Validate(_modelStub.Object);

            result.IsValid.Should().BeTrue();
        }
    }
}
