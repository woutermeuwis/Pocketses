namespace Pocketses.Core.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
public sealed class TransientDependencyAttribute : Attribute
{
}
