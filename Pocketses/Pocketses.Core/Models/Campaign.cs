using Pocketses.Core.Models.Base;

namespace Pocketses.Core.Models;

public class Campaign : AuditedEntity
{
	public string Name { get; set; }

	public string DungeonMasterId { get; set; }
	public User DungeonMaster { get; set; }

	public virtual ICollection<Character> Characters { get; set; }
	public virtual ICollection<User> Players { get; set; }

	public Campaign()
	{
		Characters = new List<Character>();
		Players = new List<User>();
	}
}
