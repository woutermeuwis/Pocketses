using Microsoft.AspNetCore.Http;
using Pocketses.Core.AppServices.Base;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Attributes;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.AppServices;

[ScopedDependency]
public class CampaignAppService : BaseAppService, ICampaignAppService
{
	private readonly ICampaignRepository _campaignRepository;
	private readonly IUserRepository _userRepository;

	public CampaignAppService(IHttpContextAccessor http, ICampaignRepository campaignRepository, IUserRepository userRepository) : base(http)
	{
		_campaignRepository = campaignRepository;
		_userRepository = userRepository;
	}

	public async Task<List<Campaign>> GetCampaignsAsync()
	{
		var user = await _userRepository.GetWithCampaignsAsync(GetUserId());
		var campaignIds = user.DmCampaigns.Union(user.UserCampaigns).Select(c => c.Id).ToArray();
		return await _campaignRepository.GetCampaignsAsync(campaignIds);
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

	public async Task<Campaign> JoinCurrentUser(Guid campaignId)
	{
		var campaign = await GetCampaignAsync(campaignId);
		var user = await _userRepository.GetAsync(GetUserId());

		campaign.Players.Add(user);
		return await _campaignRepository.UpdateAsync(campaign);
	}
}
