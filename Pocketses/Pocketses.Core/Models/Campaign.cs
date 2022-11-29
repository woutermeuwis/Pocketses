using Pocketses.Core.Models.Base;

namespace Pocketses.Core.Models;

public class Campaign : AuditedEntity
{
    public string Name { get; set; }
    
    public string DungeonMasterId { get; set; }
    public User DungeonMaster { get; set; }

    public virtual ICollection<Character> Characters { get; set; }
}
