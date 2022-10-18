namespace Pocketses.Core.Models.Specifications;
internal static class SpecificationExtensions
{
    public static ISpecification<T> And<T>(this ISpecification<T> specification, ISpecification<T> other) => new AndSpecification<T>(specification, other);
    public static ISpecification<T> AndNot<T>(this ISpecification<T> specification, ISpecification<T> other) => new AndNotSpecification<T>(specification, other);
    public static ISpecification<T> Or<T>(this ISpecification<T> specification, ISpecification<T> other) => new OrSpecification<T>(specification, other);
    public static ISpecification<T> OrNot<T>(this ISpecification<T> specification, ISpecification<T> other) => new OrNotSpecification<T>(specification, other);
    public static ISpecification<T> Not<T>(this ISpecification<T> specification) => new NotSpecification<T>(specification);
}

internal class AndSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
    }
}


internal class AndNotSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public AndNotSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return _left.IsSatisfiedBy(candidate) && !_right.IsSatisfiedBy(candidate);
    }
}


internal class OrSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return _left.IsSatisfiedBy(candidate) || _right.IsSatisfiedBy(candidate);
    }
}


internal class OrNotSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public OrNotSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return _left.IsSatisfiedBy(candidate) || !_right.IsSatisfiedBy(candidate);
    }
}


internal class NotSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _wrapped;

    public NotSpecification(ISpecification<T> wrapped)
    {
        _wrapped = wrapped;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return !_wrapped.IsSatisfiedBy(candidate);
    }
}
