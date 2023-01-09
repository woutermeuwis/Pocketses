using Microsoft.AspNetCore.Identity;

namespace Pocketses.Core.Models;

public sealed class User : IdentityUser
{
	public string Image { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }

	public ICollection<Campaign> DmCampaigns { get; set; }
	public ICollection<Campaign> UserCampaigns { get; set; }
	public ICollection<Character> Characters { get; set; }
}