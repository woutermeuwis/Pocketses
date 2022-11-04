using System.Linq.Expressions;

namespace Pocketses.Core.Models.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        private readonly List<Expression<Func<T, object>>> _includeCollection = new();

        public Expression<Func<T, bool>> Filter { get; private set; }

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public Expression<Func<T, object>> GroupBy { get; private set; }

        public List<Expression<Func<T, object>>> Includes => _includeCollection;

        public BaseSpecification()
        { }

        public BaseSpecification(Expression<Func<T, bool>> filter)
        {
            Filter = filter;
        }

        protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
        protected void ApplyOrderBy(Expression<Func<T, object>> orderBy) => OrderBy = orderBy;
        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending) => OrderByDescending = orderByDescending;
        protected void SetFilter(Expression<Func<T, bool>> filter) => Filter = filter;
        protected void ApplyGroupBy(Expression<Func<T, object>> groupBy) => GroupBy = groupBy;

    }
}
