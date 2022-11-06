using System.ComponentModel.DataAnnotations;

namespace Pocketses.Web.Models.Campaigns;

public class Campaign
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
}
