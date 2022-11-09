using Microsoft.AspNetCore.Mvc;
using Pocketses.Core.AppServices.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Pocketses.Api.Controllers;

[ApiController]
[Route("authenticate")]
public class AuthController : ControllerBase
{
    private readonly IUserAppService _userAppService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, IUserAppService userAppService)
    {
        _logger = logger;
        _userAppService = userAppService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] string token)
    {
        token = await _userAppService.SetUser(token);
        return token == null
            ? BadRequest()
            : Ok(token);
    }
}
