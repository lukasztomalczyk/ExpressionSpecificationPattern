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
                new ExpressionTreeEF.User{ ID = 3, FirstName = "Kevin", LastName = "Durant"}
            };

            var search = new List<SearchCriteria>
            {
                new SearchCriteria { Field = "FirstName", Value = "Kevin", Operation = OperationsEnum.ALL },
                new SearchCriteria { Field = "FirstName", Value = "Kevin", Operation = OperationsEnum.AND }
            };




            var user = userList.AsQueryable().Where(SearchQueryExpression.GetDynamicQueryWithExpresionTrees("FirstName", "Kevin")).ToList();
            //var query = UserSearchQuerySpecification.UserSearch(search);
            //var filter = userList.AsQueryable().Where(UserSearchQuerySpecification.UserSearch(search).Compile()).ToList();
            Console.WriteLine("ok");
        }
    }
}
