using System;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> left;
        private readonly Specification<T> right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            this.left = left;
            this.right = right;
        }
        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = left.ToExpression();
            Expression<Func<T, bool>> rightExpression = right.ToExpression();

            ParameterExpression param = leftExpression.Parameters[0];
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    leftExpression.Body,
                    Expression.Invoke(rightExpression, param)), param);
        }
    }
}