using Microsoft.AspNetCore.Mvc;
using Pocketses.Core.AppServices.Interfaces;

namespace Pocketses.Api.Controllers;

/// <summary>
/// Authorization
/// </summary>
[ApiController]
[Route("authenticate")]
public class AuthController : BaseController
{
	private readonly IUserAppService _userAppService;

	/// <inheritdoc/>
	public AuthController(IHttpContextAccessor http, IUserAppService userAppService) : base(http)
	{
		_userAppService = userAppService;
	}

	/// <summary>
	/// Logs in user with google auth
	/// </summary>
	/// <param name="token">Google auth token</param>
	/// <response code="200">Returns JWT Access token</response>
	/// <response code="400">Invalid token</response>
	[HttpPost]
	public async Task<ActionResult<string>> Post([FromBody] string token)
	{
		token = await _userAppService.SetUser(token);
		return token == null
			? BadRequest()
			: Ok(token);
	}
}
