using UrlShortener.Infrastructure;

namespace UrlShortener.WebApi.Lib.Hal
{
    public class NextBase<T>
    {
        protected readonly Paged<T> Model;

        protected dynamic Parameters => new
        {
            skip = Model.Skip + 1,
            limit = 100
        };

        public NextBase(Paged<T> model)
        {
            Model = model;
        }

        public static bool Predicate(Paged<T> model)
        {
            return model.Skip < model.Pages;
        }
    }
}