namespace UrlShortener.WebApi.Infrastructure
{
    public class Shortener
    {
        private const string Base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

        public virtual string Shorten(string text)
        {
            var hashCode = text.GetHashCode();

            var encoded = ConvertToBase(hashCode, string.Empty);

            return encoded;
        }

        private static string ConvertToBase(int number, string encodedString)
        {
            const int @base = 64;
            var chars = Base64Chars.ToCharArray();

            if (number < @base)
            {
                encodedString = encodedString + chars[number];
            }
            else
            {
                var newNumber = number / @base;
                encodedString = ConvertToBase(newNumber, encodedString);

                number = number - (newNumber * @base);

                if (number < @base)
                {
                    encodedString = encodedString + chars[number];
                }
            }

            return encodedString;
        }
    }
}
