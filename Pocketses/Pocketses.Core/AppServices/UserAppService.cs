using System.Globalization;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Attributes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Pocketses.Core.Models;

namespace Pocketses.Core.AppServices;

[ScopedDependency]
public sealed class UserAppService : IUserAppService
{
	private readonly UserManager<User> _userManager;
	private readonly IConfiguration _configuration;

	public UserAppService(UserManager<User> userManager, IConfiguration configuration)
	{
		_userManager = userManager;
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
		if (user is null)
		{
			user = await _userManager.FindByEmailAsync(payload.Email);
			if (user is null)
			{
				user = new User
				{
					Email = payload.Email,
					UserName = payload.Email,
					FirstName = payload.GivenName,
					LastName = payload.FamilyName,
					Image = payload.Picture
				};
				await _userManager.CreateAsync(user);
			}
			await _userManager.AddLoginAsync(user, info);
		}
		return GetToken(user);
	}

	private string GetToken(User user)
	{
		var utcNow = DateTime.UtcNow;
		var claims = new Claim[]
		{
			new(JwtRegisteredClaimNames.Sub, user.Id),
			new(JwtRegisteredClaimNames.UniqueName, user.UserName),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new(JwtRegisteredClaimNames.Iat, utcNow.ToString(CultureInfo.InvariantCulture)),
			new("picture", user.Image),
			new(JwtRegisteredClaimNames.GivenName, user.FirstName),
			new(JwtRegisteredClaimNames.FamilyName, user.LastName),
			new(JwtRegisteredClaimNames.Email, user.Email)
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