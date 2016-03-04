using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Hal
{
    public class PrevBase<T>
    {
        protected readonly Paged<T> Model;

        protected dynamic Parameters => new
        {
            skip = Model.Skip - 1,
            limit = 100
        };

        public PrevBase(Paged<T> model)
        {
            Model = model;
        }

        public static bool Predicate(Paged<T> model)
        {
            return model.Skip > 0;
        }
    }
}