using Pocketses.Core.Models;

namespace Pocketses.Core.AppServices.Interfaces;

public interface ICampaignAppService
{
    Task<List<Campaign>> GetCampaignsAsync(string searchFilter);
    Task<Campaign> GetCampaignAsync(Guid id);
    Task CreateAsync(Campaign campaign);
    Task UpdateAsync(Campaign toUpdate);
    Task DeleteAsync(Guid value);
}
