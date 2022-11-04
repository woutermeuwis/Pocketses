using System.Linq.Expressions;

namespace Pocketses.Core.Models.Specifications;
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Filter { get; }

    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }

    List<Expression<Func<T, object>>> Includes { get; }

    Expression<Func<T, object>> GroupBy { get; }

}
