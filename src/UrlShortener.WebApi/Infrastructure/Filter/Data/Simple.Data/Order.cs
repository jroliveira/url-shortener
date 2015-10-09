using Simple.Data;

namespace UrlShortener.WebApi.Infrastructure.Filter.Data.Simple.Data
{
    public class Order : IOrder<ObjectReference>
    {
        public ObjectReference Apply(Filter filter)
        {
            var owner = ObjectReference.FromString(filter.Resource);
            var name = filter.Order.Property;

            var order = new ObjectReference(name, owner);

            return order;
        }
    }
}