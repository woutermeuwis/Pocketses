using AutoMapper;
using Pocketses.Core.Models.Base;
using System.Linq.Expressions;

namespace Pocketses.Core.Extensions
{
    public static class MapperExtensions
    {
        public static IMappingExpression<TSource, TDest> Ignore<TSource, TDest, TProperty>(this IMappingExpression<TSource, TDest> map, Expression<Func<TDest, TProperty>> destinationMember)
        {
            return map.ForMember(destinationMember, options => options.Ignore());
        }

        public static IMappingExpression<TSource, TDest> IgnoreAuditedEntity<TSource, TDest>(this IMappingExpression<TSource, TDest> map) where TDest : AuditedEntity
        {
            return map
                .Ignore(d => d.CreatedBy)
                .Ignore(d => d.CreatedAtUtc)
                .Ignore(d => d.UpdatedBy)
                .Ignore(d => d.UpdatedAtUtc);
        }

        public static IMappingExpression<TSource, TDest> IgnoreId<TSource, TDest>(this IMappingExpression<TSource, TDest> map) where TDest : Entity
        {
            return map.Ignore(d => d.Id);
        }
    }
}
