using Microsoft.EntityFrameworkCore;

namespace Pocketses.Core.Models.Specifications.Campaigns
{
    internal class CampaignNameSpecification : BaseSpecification<Campaign>
    {
        public CampaignNameSpecification(string name)
            : base(c => EF.Functions.Like(c.Name, $"%{name}%"))
        { }
    }
}
