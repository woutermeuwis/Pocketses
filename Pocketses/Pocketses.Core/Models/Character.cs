using Pocketses.Core.Models.Base;

namespace Pocketses.Core.Models;

public class Character : AuditedEntity
{
	public string UserId { get; set; }
	public string Name { get; set; }
}
