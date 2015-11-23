using System.Linq;
using Simple.Data;

namespace UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data
{
    public class Order : IOrder<Filter, ObjectReference>
    {
        public ObjectReference Apply(Filter filter)
        {
            var owner = ObjectReference.FromString(filter.Resource);
            var name = filter.Order.Fields.First().Name;

            var order = new ObjectReference(name, owner);

            return order;
        }
    }
}