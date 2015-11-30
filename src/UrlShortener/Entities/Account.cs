using UrlShortener.Infrastructure.Security;

namespace UrlShortener.Entities
{
    public class Account : Entity<int>
    {
        private readonly IHashAlgorithm _hashAlgorithm;

        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }

        internal Account(IHashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public Account()
            : this(new Md5HashAlgorithm())
        {

        }

        public virtual void HashPassword()
        {
            Password = _hashAlgorithm.Hash(Password);
        }

        public virtual bool ValidatePassword(string password)
        {
            password = _hashAlgorithm.Hash(password);

            return Password == password;
        }
    }
}
