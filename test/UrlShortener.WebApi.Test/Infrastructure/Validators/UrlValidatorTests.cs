using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.WebApi.Infrastructure.Validators;
using UrlShortener.WebApi.Models.Url;
using UrlShortener.WebApi.Models.Url.Post;

namespace UrlShortener.WebApi.Test.Infrastructure.Validators
{
    [TestFixture]
    public class UrlValidatorTests
    {
        private UrlValidator _validator;
        private Mock<Url> _modelStub;

        [SetUp]
        public void SetUp()
        {
            _validator = new UrlValidator();

            _modelStub = new Mock<Url>();
            _modelStub.Setup(m => m.Address).Returns("http://jroliveira.net");
            _modelStub.Setup(e => e.Account.Id).Returns(1);
        }

        [Test]
        public void Validade_GivenUrlIsNull_ShouldReturnFalse()
        {
            _modelStub.Setup(m => m.Address).Returns(default(string));

            var result = _validator.Validate(_modelStub.Object);

            result
                .Errors
                .Any(c => c.ErrorMessage.Equals("Endereço deve ser informado."))
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validade_GivenUrlIsNotNull_ShouldReturnTrue()
        {
            _modelStub.Setup(m => m.Address).Returns("http://jroliveira.net");

            var result = _validator.Validate(_modelStub.Object);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validade_GivenAccountIsNull_ShouldReturnFalse()
        {
            _modelStub.Setup(m => m.Account).Returns(default(Account));

            var result = _validator.Validate(_modelStub.Object);

            result
                .Errors
                .Any(c => c.ErrorMessage.Equals("Conta deve ser informada."))
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validade_GivenAccountIdLessThan1_ShouldReturnFalse()
        {
            _modelStub.Setup(m => m.Account.Id).Returns(0);

            var result = _validator.Validate(_modelStub.Object);

            result
                .Errors
                .Any(c => c.ErrorMessage.Equals("Id da conta deve ser maior que zero."))
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validade_GivenAccountIdGreaterThan0_ShouldReturnTrue()
        {
            _modelStub.Setup(m => m.Account.Id).Returns(1);

            var result = _validator.Validate(_modelStub.Object);

            result.IsValid.Should().BeTrue();
        }
    }
}
