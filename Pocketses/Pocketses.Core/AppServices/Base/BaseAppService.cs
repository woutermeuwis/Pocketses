using Microsoft.AspNetCore.Http;
using Pocketses.Core.Extensions;

namespace Pocketses.Core.AppServices.Base;
public abstract class BaseAppService
{
	private readonly IHttpContextAccessor _http;

	public BaseAppService(IHttpContextAccessor http)
	{
		_http = http;
	}

	protected string GetUserId() => _http.GetUserId();
}
