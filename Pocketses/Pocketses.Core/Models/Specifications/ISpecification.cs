namespace Pocketses.Core.Models.Specifications;
internal interface ISpecification<T>
{
    bool IsSatisfiedBy(T Candidate);
}
