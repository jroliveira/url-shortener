namespace UrlShortener.WebApi.Infrastructure.Filter.Where
{
    public class Property
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}