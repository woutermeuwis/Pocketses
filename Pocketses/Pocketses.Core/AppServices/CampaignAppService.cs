using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Attributes;
using Pocketses.Core.Models;
using Pocketses.Core.Models.Specifications.Campaigns;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.AppServices;

[ScopedDependency]
public class CampaignAppService : ICampaignAppService
{
	private readonly ICampaignRepository _campaignRepository;

	public CampaignAppService(ICampaignRepository campaignRepository)
	{
		_campaignRepository = campaignRepository;
	}

	public Task<List<Campaign>> GetCampaignsAsync(string searchFilter)
	{
		return string.IsNullOrEmpty(searchFilter)
			? _campaignRepository.GetAllAsync()
			: _campaignRepository.GetAllAsync(new CampaignNameSpecification(searchFilter));
	}

	public Task<Campaign> GetCampaignAsync(Guid id)
	{
		return _campaignRepository.GetAsync(id);
	}

	public Task<Campaign> CreateAsync(Campaign campaign)
	{
		return _campaignRepository.CreateAsync(campaign);
	}

	public Task<Campaign> UpdateAsync(Campaign toUpdate)
	{
		return _campaignRepository.UpdateAsync(toUpdate);
	}

	public Task DeleteAsync(Guid id)
	{
		return _campaignRepository.DeleteAsync(id);
	}
}
