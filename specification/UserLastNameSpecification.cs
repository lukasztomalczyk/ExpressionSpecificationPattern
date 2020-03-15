using System;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    public class UserLastNameSpecification : Specification<ExpressionTreeEF.User>
    {
        private readonly string value;

        public UserLastNameSpecification(string value)
        {
            this.value = value;
        }
        public override Expression<Func<ExpressionTreeEF.User, bool>> ToExpression()
        {
            return x => x.LastName == value;
        }
    }
}