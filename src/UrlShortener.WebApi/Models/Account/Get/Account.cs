namespace UrlShortener.WebApi.Models.Account.Get
{
    public class Account
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
    }
}