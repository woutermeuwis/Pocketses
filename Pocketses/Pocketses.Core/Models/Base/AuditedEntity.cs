namespace Pocketses.Core.Models.Base;

public class AuditedEntity : Entity
{
    public DateTime CreatedAtUtc { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public Guid UpdatedBy {get;set;}
}
