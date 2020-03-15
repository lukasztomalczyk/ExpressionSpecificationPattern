using System;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    public class UserFirstNameSpecification : Specification<ExpressionTreeEF.User>
    {
        private readonly string value;

        public  UserFirstNameSpecification(string value)
        {
            this.value = value;
        }
        public override Expression<Func<ExpressionTreeEF.User, bool>> ToExpression()
        {
            return x => x.FirstName == value;
        }
    }
}