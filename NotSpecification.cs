using System;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> specification;

        public NotSpecification(Specification<T> specification)
        {
            this.specification = specification;
        }
        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = specification.ToExpression();

            UnaryExpression orExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(orExpression, expression.Parameters.First());
        }
    }
}