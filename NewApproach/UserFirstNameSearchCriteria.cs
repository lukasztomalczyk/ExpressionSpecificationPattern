using System;
using System.Linq.Expressions;
using ExpressionTreeEF.NewApprouche;

namespace ExpressionTreeEF
{
    public class UserFirstNameSearchCriteria : IExpressionCriteria<User>
    {
        private readonly string value;
        private readonly OperationsEnum operation;

        public UserFirstNameSearchCriteria(string value, OperationsEnum operation)
        {
            this.value = value;
            this.operation = operation;
        }

        public Expression<Func<User, bool>> ToExpression()
        {
            var param = Expression.Parameter(typeof(User), "x");
            var member = Expression.Property(param, "FirstName");
            //val ("Curry")
            var valExpression = Expression.Constant(value, typeof(string));
            //x.LastName == "Curry"
            Expression body = Expression.Equal(member, valExpression);
            //x => x.LastName == "Curry"
            var final = Expression.Lambda<Func<User, bool>>(body: body, parameters: param);
            //compiles the expression tree to a func delegate
            return final;
        }
    }
}