namespace UrlShortener.Entities
{
    public class Url : Entity<int>
    {
        public virtual string Address { get; set; }
        public virtual Account Account { get; set; }
        public virtual string Shortened { get; private set; }

        public virtual void Shorten()
        {
            Shortened = $"{Address.GetHashCode():X}".ToLower();
        }
    }
}
