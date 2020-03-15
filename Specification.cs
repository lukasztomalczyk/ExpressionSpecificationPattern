using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new IdentitySpecification<T>();
        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;

            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (specification == All || this == All)
                return All;

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not() 
        {
            return new NotSpecification<T>(this);
        }

        protected Expression<Func<TParam, bool>> CreateEqulaExpression<TParam>(string fieldName, string fieldValue)
        {
            var param = Expression.Parameter(typeof(TParam));
            var member = Expression.Property(param, fieldName);
            var constant = Expression.Constant(fieldValue);
            var body = Expression.Equal(member, constant);

            return Expression.Lambda<Func<TParam, bool>>(body: body, parameters: param);
        }

        protected Expression<Func<TParam, bool>> AndExpression<TParam>(Expression<Func<TParam, bool>> leftExpr, Expression<Func<TParam, bool>> rightExpr)
        {
            ParameterExpression param = leftExpr.Parameters[0];

            return Expression.Lambda<Func<TParam, bool>>(
                Expression.AndAlso(
                    leftExpr.Body,
                    Expression.Invoke(rightExpr, param)), param);
        }

        protected Expression<Func<TParam, bool>> OrExpression<TParam>(Expression<Func<TParam, bool>> leftExpr, Expression<Func<TParam, bool>> rightExpr)
        {
            ParameterExpression param = leftExpr.Parameters[0];

            return Expression.Lambda<Func<TParam, bool>>(
                Expression.OrElse(
                    leftExpr.Body,
                    Expression.Invoke(rightExpr, param)), param);
        }
    }
}