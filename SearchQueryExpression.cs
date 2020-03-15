using System;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    public static class SearchQueryExpression
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

        public static Expression<Func<User, bool>> Test()
        {
                        ParameterExpression param = Expression.Parameter(typeof(User));
            MemberExpression member = Expression.Property(param, "FirstName");
            ConstantExpression constant = Expression.Constant("Kevin");
            BinaryExpression body = Expression.Equal(member, constant);

           var leftExp = Expression.Lambda<Func<string, bool>>(body: body, parameters: param);

            //Expression<Func<User, bool>> leftExp = x => x.FirstName == "Kevin";
            Expression<Func<User, bool>> rightExp = x => x.LastName == "Durant";

            //ParameterExpression param = leftExp.Parameters[0];
            return Expression.Lambda<Func<User, bool>>(
                Expression.AndAlso(
                    leftExp.Body,
                    Expression.Invoke(rightExp, param)), param);

            if (ReferenceEquals(param, rightExp.Parameters[0]))
            {
                // simple version
                return Expression.Lambda<Func<User, bool>>(
                    Expression.AndAlso(leftExp.Body, rightExp.Body), param);
            }

        //    // var param = Expression.Parameter(typeof(User), "x");
        //     Expression body = Expression.AndAlso(
        //             Expression.Invoke(leftExp, leftExp.Parameters.Single()),
        //             Expression.Invoke(rightExp, rightExp.Parameters.Single()));
        //    // Expression andExp = Expression.AndAlso(leftExp.Body, rightExp.Body);
        //     var lambda = Expression.Lambda<Func<User, bool>>(body, param);
        //         return lambda;
        }

        // public static Expression<Func<T, bool>> AndAlso2<T>(
        //     this Expression<Func<T, bool>> expr1,
        //     Expression<Func<T, bool>> expr2)
        // {
        //     // need to detect whether they use the same
        //     // parameter instance; if not, they need fixing
        //     ParameterExpression param = expr1.Parameters[0];
        //     if (ReferenceEquals(param, expr2.Parameters[0]))
        //     {
        //         // simple version
        //         return Expression.Lambda<Func<T, bool>>(
        //             Expression.AndAlso(expr1.Body, expr2.Body), param);
        //     }
        //     // otherwise, keep expr1 "as is" and invoke expr2
        //     return Expression.Lambda<Func<T, bool>>(
        //         Expression.AndAlso(
        //             expr1.Body,
        //             Expression.Invoke(expr2, param)), param);
        // }
    }
}