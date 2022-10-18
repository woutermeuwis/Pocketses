using Pocketses.Core.Models.Base;

namespace Pocketses.Core.Models;

public class Player : AuditedEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Campaign Campaign { get; set; }
    public virtual ICollection<Character> Characters {get;set;}
}
