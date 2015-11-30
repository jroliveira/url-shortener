using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.Entities;
using UrlShortener.Infrastructure.Security;

namespace UrlShortener.Test.Entities
{
    [TestFixture]
    public class AccountTests
    {
        private Account _account;
        private Mock<IHashAlgorithm> _hashAlgorithmMock;

        [SetUp]
        public void SetUp()
        {
            _hashAlgorithmMock = new Mock<IHashAlgorithm>();

            _account = new Account(_hashAlgorithmMock.Object);
        }

        [Test]
        public void HashPassword_DeveChamarHashAlgorithmUmaVez()
        {
            _account.HashPassword();

            _hashAlgorithmMock.Verify(m => m.Hash(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void HashPassword_DevePreencherPropriedadeShortened()
        {
            _hashAlgorithmMock
                .Setup(m => m.Hash(It.IsAny<string>()))
                .Returns("123");

            _account.HashPassword();

            _account.Password.Should().Be("123");
        }

        [Test]
        public void ValidatePassword_DeveChamarHashAlgorithmUmaVez()
        {
            _account.ValidatePassword(It.IsAny<string>());

            _hashAlgorithmMock.Verify(m => m.Hash(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ValidatePassword_DadaSenhaDiferenteDaSenhaAtual_DeveRetornarFalse()
        {
            _hashAlgorithmMock
                .Setup(m => m.Hash(It.IsAny<string>()))
                .Returns("123");

            var actual = _account.ValidatePassword(It.IsAny<string>());

            actual.Should().BeFalse();
        }

        [Test]
        public void ValidatePassword_DadaSenhaIgualSenhaAtual_DeveRetornarTrue()
        {
            _account.Password = "123";

            _hashAlgorithmMock
                .Setup(m => m.Hash(It.IsAny<string>()))
                .Returns("123");

            var actual = _account.ValidatePassword(It.IsAny<string>());

            actual.Should().BeTrue();
        }
    }
}
