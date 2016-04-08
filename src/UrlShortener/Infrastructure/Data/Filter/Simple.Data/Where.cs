using System.Collections.Generic;
using System.Linq;
using Restful.Query.Filter.Filters.Condition.Operators;
using Simple.Data;

namespace UrlShortener.Infrastructure.Data.Filter.Simple.Data
{
    public class Where : IWhere<Filter, SimpleExpression>
    {
        public SimpleExpression Apply(Filter filter)
        {
            var leftOperand = GetLeftOperator(filter);
            var rightOperand = filter.Where.Last().Value;

            var @operator = GetOperator(filter);

            return new SimpleExpression(leftOperand, rightOperand, @operator);
        }

        private static SimpleExpressionType GetOperator(Restful.Query.Filter.Filter filter)
        {
            var operations = new Dictionary<Comparison, SimpleExpressionType>
            {
                { Comparison.GreaterThan, SimpleExpressionType.GreaterThan },
                { Comparison.LessThan, SimpleExpressionType.LessThan },
                { Comparison.Equal, SimpleExpressionType.Equal }
            };

            return operations[filter.Where.First().Comparison];
        }

        private static object GetLeftOperator(Filter filter)
        {
            var owner = ObjectReference.FromString(filter.Resource);
            var name = filter.Where.First().Name;

            return new ObjectReference(name, owner);
        }
    }
}