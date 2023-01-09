using Pocketses.Core.Models;

namespace Pocketses.Core.Repositories.Interfaces;

public interface ICampaignRepository : IRepository<Campaign>
{
	Task<List<Campaign>> GetCampaignsAsync(Guid[] ids);
}
