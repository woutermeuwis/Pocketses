using Pocketses.Core.Models;

namespace Pocketses.Core.AppServices.Interfaces;

public interface ICampaignAppService
{
	Task<List<Campaign>> GetCampaignsAsync();
	Task<Campaign> GetCampaignAsync(Guid id);
	Task<Campaign> CreateAsync(Campaign campaign);
	Task<Campaign> UpdateAsync(Campaign toUpdate);
	Task DeleteAsync(Guid value);
	Task<Campaign> JoinCurrentUser(Guid campaignId);
}
