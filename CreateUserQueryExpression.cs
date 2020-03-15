using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    public class CreateUserQueryExpression : Specification<User>
    {
        private readonly List<SearchCriteria> criteriaList;

        public CreateUserQueryExpression(List<SearchCriteria> criteriaList)
        {
            this.criteriaList = criteriaList;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            var userPropertiesNameList = GetStringFieldsNames();

            Expression<Func<User, bool>> allExpression = x => true;

            foreach (var criteria in criteriaList)
            {
                if (!userPropertiesNameList.Any(fieldName => fieldName == criteria.Field))
                    throw new Exception($"Field name not supported: {criteria.Field}");

                var expression = this.CreateEqulaExpression<User>(criteria.Field, criteria.Value);

                if (criteria.Operation == OperationsEnum.ALL | criteria.Operation == OperationsEnum.AND)
                    allExpression = this.AndExpression(allExpression, expression);
                if (criteria.Operation == OperationsEnum.OR)
                    allExpression = this.OrExpression(allExpression, expression);
            }

            return allExpression;
        }

        protected IReadOnlyList<string> GetStringFieldsNames()
            => typeof(User).GetProperties().Where(field => field.PropertyType.FullName.Equals(typeof(string).FullName)).Select(field => field.Name).ToList();
    }
}