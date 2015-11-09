namespace UrlShortener.WebApi.Models.Account.Post
{
    public class Account
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string ConfirmPassword { get; set; }
    }
}