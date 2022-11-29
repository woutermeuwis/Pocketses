using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Pocketses.Core.Extensions;

public static class HttpContextAccessorExtensions
{
	public static string GetUserId(this IHttpContextAccessor accessor)
	{
		return accessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
	}
}