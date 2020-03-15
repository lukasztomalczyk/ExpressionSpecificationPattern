using System;
using System.Linq.Expressions;

namespace ExpressionTreeEF.NewApprouche
{
    public interface IExpressionCriteria<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}