namespace Pocketses.Core.Models.Base;

public class AuditedEntity : Entity
{
    public DateTime CreatedAtUtc { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public string UpdatedBy {get;set;}
}
