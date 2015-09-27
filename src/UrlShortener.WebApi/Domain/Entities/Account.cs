using UrlShortener.WebApi.Infrastructure.Security;

namespace UrlShortener.WebApi.Domain.Entities
{
    public class Account : Entity<int>
    {
        private readonly IHashAlgorithm _hashAlgorithm;

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Account()
        {
            _hashAlgorithm = new Md5HashAlgorithm();
        }

        public void HashPassword()
        {
            Password = _hashAlgorithm.Hash(Password);
        }

        public bool ValidatePassword(string password)
        {
            password = _hashAlgorithm.Hash(password);

            return Password == password;
        }
    }
}
