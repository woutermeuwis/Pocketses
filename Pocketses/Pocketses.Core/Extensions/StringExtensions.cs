namespace Pocketses.Core.Extensions;
public static class StringExtensions
{
	public static bool IsNullOrEmpty(this string str) => str is null or { Length: 0 };
	public static bool IsNotNullOrEmpty(this string str) => str is { Length: > 0 };

	public static bool IsGuid(this string str) => Guid.TryParse(str, out _);
	public static Guid ToGuid(this string str) => Guid.Parse(str);
}
