namespace Pocketses.Core.Models.Specifications;
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T Candidate);
}
