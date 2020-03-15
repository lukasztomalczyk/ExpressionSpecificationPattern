using System;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    public class SearchQueryExpression
    {
        public static Expression<Func<User, bool>> GetDynamicQueryWithExpresionTrees(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(User), "x");
            var member = Expression.Property(param, propertyName);
            //val ("Curry")
            var valExpression = Expression.Constant(val, typeof(string));
            //x.LastName == "Curry"
            Expression body = Expression.Equal(member, valExpression);
            //x => x.LastName == "Curry"
            var final = Expression.Lambda<Func<User, bool>>(body: body, parameters: param);
            //compiles the expression tree to a func delegate
            return final;
        }
    }
}