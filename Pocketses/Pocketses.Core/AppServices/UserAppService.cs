using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Attributes;
using Pocketses.Core.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pocketses.Core.AppServices;

[ScopedDependency]
public sealed class UserAppService : IUserAppService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UserAppService(IUserRepository userRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<string> SetUser(string token)
    {
        if (token is null)
            throw new ArgumentNullException(nameof(token));

        var validationSettings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string>() { "661185425102-p3r127p52ftmtmeu4i8cn9fm1q0k5iv8.apps.googleusercontent.com" } // google client id
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(token, validationSettings);
        if (payload is null)
            return null;

        var info = new UserLoginInfo("Google", payload.Subject, payload.Email);
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        if(user is null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if(user is null)
            {
                user = new IdentityUser { Email = payload.Email, UserName = payload.Email };
                await _userManager.CreateAsync(user);
            }

            await _userManager.AddLoginAsync(user, info);
        }

        if (user is null)
            return null;

        
        return GetToken(user);
    }

    private string GetToken(IdentityUser user)
    {
        var utcNow = DateTime.UtcNow;
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),
        };

        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
        var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            signingCredentials: signinCredentials,
            claims: claims,
            notBefore: utcNow,
            expires: utcNow.AddSeconds(int.Parse(_configuration["Tokens:Lifetime"])),
            audience: _configuration["Tokens:Audience"],
            issuer: _configuration["Tokens:Issuer"]
            );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
