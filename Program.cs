using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionTreeEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var userList = new List<ExpressionTreeEF.User>
            {
                new ExpressionTreeEF.User{ ID = 1, FirstName = "Kevin", LastName = "Garnett"},
                new ExpressionTreeEF.User{ ID = 2, FirstName = "Stephen", LastName = "Curry"},
                new ExpressionTreeEF.User{ ID = 3, FirstName = "Kevin", LastName = "Durant"},
                new ExpressionTreeEF.User{ ID = 3, FirstName = "Kevin", LastName = "lukasz"},
                new ExpressionTreeEF.User{ ID = 3, FirstName = "Kevin1", LastName = "lukasz2"},
                new ExpressionTreeEF.User{ ID = 3, FirstName = "Kevin2", LastName = "lukas3z"}
            };

            var search = new List<SearchCriteria>
            {
                new SearchCriteria { Field = "FirstName", Value = "Kevin", Operation = OperationsEnum.ALL },
                new SearchCriteria { Field = "LastName", Value = "Durant", Operation = OperationsEnum.AND },
                new SearchCriteria { Field = "LastName", Value = "lukasz", Operation = OperationsEnum.OR }
            };




           // var user = userList.AsQueryable().Where(SearchQueryExpression.Test()).ToList();
            var query = UserSearchQuerySpecification.UserSearch(search);;
            var filter = userList.AsQueryable().Where(UserSearchQuerySpecification.UserSearch(search)).ToList();
            var filter2 = userList.AsQueryable().Where(new CreateUserQueryExpression(search).ToExpression()).ToList();
            Console.WriteLine("ok");
        }
    }
}
