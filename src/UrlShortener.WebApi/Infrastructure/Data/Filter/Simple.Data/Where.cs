using System.Collections.Generic;
using Restful.Query.Filter.Where;
using Simple.Data;

namespace UrlShortener.WebApi.Infrastructure.Data.Filter.Simple.Data
{
    public class Where : IWhere<Filter, SimpleExpression>
    {
        public SimpleExpression Apply(Filter filter)
        {
            var leftOperator = GetLeftOperator(filter);
            var rightOperator = filter.Where.Property.Value;

            var @operator = GetOperator(filter);

            return new SimpleExpression(leftOperator, rightOperator, @operator);
        }

        private static SimpleExpressionType GetOperator(Filter filter)
        {
            var operations = new Dictionary<Operator, SimpleExpressionType>
            {
                { Operator.GreaterThan, SimpleExpressionType.GreaterThan },
                { Operator.LessThan, SimpleExpressionType.LessThan }
            };

            return operations[filter.Where.Operator];
        }

        private static object GetLeftOperator(Filter filter)
        {
            var owner = ObjectReference.FromString(filter.Resource);
            var name = filter.Where.Property.Name;

            return new ObjectReference(name, owner);
        }
    }
}