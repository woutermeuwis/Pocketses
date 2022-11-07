using Pocketses.Core.Models.Specifications;
using System.Data.Entity;

namespace Pocketses.Core.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<TEntity> EvaluateSpecification<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification is null || (query is null))
                return query;

            if(specification.Filter is not null)
                query = query.Where(specification.Filter);

            if (specification.Includes is not null)
                query = specification.Includes.Aggregate(query, (c, i) => c.Include(i));

            if(specification.OrderBy is not null)
                query.OrderBy(specification.OrderBy);
            else if(specification.OrderByDescending is not null)
                query.OrderByDescending(specification.OrderByDescending);

            if(specification.GroupBy is not null)
                query.GroupBy(specification.GroupBy).SelectMany(x=>x);

            return query;
        }
    }
}
