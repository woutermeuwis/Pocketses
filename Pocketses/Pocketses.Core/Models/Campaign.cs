using Pocketses.Core.Models.Base;

namespace Pocketses.Core.Models;

public class Campaign : AuditedEntity
{
    public string Name { get; set; }

    public virtual ICollection<Player> Players {get;set;}
    public virtual ICollection<Character> Characters { get; set; }
}
