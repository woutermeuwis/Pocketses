using Pocketses.Core.Models.Base;

namespace Pocketses.Core.Models;

public class Character : AuditedEntity
{
	public string Name { get; set; }

	public string UserId { get; set; }
	public User User { get; set; }

	public Guid CampaignId { get; set; }
	public Campaign Campaign { get; set; }
}
