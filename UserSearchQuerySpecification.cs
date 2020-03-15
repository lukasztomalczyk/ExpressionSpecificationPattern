using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    public static class UserSearchQuerySpecification
    {
        public static Expression<Func<ExpressionTreeEF.User, bool>> UserSearch(List<SearchCriteria> criteria)
        {
            Specification<ExpressionTreeEF.User> includes = Specification<ExpressionTreeEF.User>.All;
  
            foreach (var item in criteria)
            {
                switch (item.Field)
                {
                    case "FirstName":
                        {
                            if(item.Operation == OperationsEnum.ALL)
                               includes = includes.And(new UserFirstNameSpecification(item.Value));
                            else if (item.Operation == OperationsEnum.AND)
                                includes = includes.And(new UserFirstNameSpecification(item.Value));
                            else
                                includes = includes.Or(new UserFirstNameSpecification(item.Value));
                        }
                        break;
                    case "LastName":
                        {
                            if(item.Operation == OperationsEnum.ALL)
                                includes = includes.And(new UserLastNameSpecification(item.Value));
                            else if (item.Operation == OperationsEnum.AND)
                                includes = includes.And(new UserLastNameSpecification(item.Value));
                            else
                                includes = includes.Or(new UserLastNameSpecification(item.Value));
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }
            return includes.ToExpression();
        }

        private static Expression<Func<string, bool>> AllExpression(string field, string value)
        {
            ParameterExpression param = Expression.Parameter(typeof(string));
            MemberExpression member = Expression.Property(param, field);
            ConstantExpression constant = Expression.Constant(value);
            BinaryExpression body = Expression.Equal(member, constant);

            return Expression.Lambda<Func<string, bool>>(body: body, parameters: param);
        }
    }
}