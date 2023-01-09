using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Pocketses.Core.Extensions;

namespace Pocketses.Api.Controllers;

public class BaseController : ControllerBase
{
	private readonly IHttpContextAccessor _http;

	public BaseController(IHttpContextAccessor http)
	{
		_http = http;
	}

	public override CreatedAtActionResult CreatedAtAction(string? actionName, object? routeValues, [ActionResultObjectValue] object? value)
	{
		return base.CreatedAtAction(CleanActionName(actionName), routeValues, value);
	}

	public override CreatedAtActionResult CreatedAtAction(string? actionName, [ActionResultObjectValue] object? value)
	{
		return base.CreatedAtAction(CleanActionName(actionName), value);
	}

	public override CreatedAtActionResult CreatedAtAction(string? actionName, string? controllerName, object? routeValues, [ActionResultObjectValue] object? value)
	{
		return base.CreatedAtAction(CleanActionName(actionName), controllerName, routeValues, value);
	}

	protected string GetUserId() => _http.GetUserId();

	private string? CleanActionName(string? actionName) => (actionName ?? "").EndsWith("Async") ? actionName?[..^5] : actionName;
}
