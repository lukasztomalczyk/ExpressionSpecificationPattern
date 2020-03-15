using System;
using System.Linq.Expressions;

namespace ExpressionTreeEF
{
    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new IdentitySpecification<T>();
        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;

            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (specification == All || this == All)
                return All;

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not() 
        {
            return new NotSpecification<T>(this);
        }
    }
}