using Pocketses.Core.Models;

namespace Pocketses.Core.AppServices.Interfaces;

public interface ICampaignAppService 
{
    Task<List<Campaign>> GetCampaignsAsync(string searchFilter);
    Task<Campaign> GetCampaignAsync(Guid id);
    Task<Campaign> CreateAsync(Campaign campaign);
    Task<Campaign> UpdateAsync(Campaign toUpdate);
    Task DeleteAsync(Guid value);
}
