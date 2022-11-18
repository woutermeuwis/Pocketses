using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Dto.Campaign;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CampaignsController : ControllerBase
{
	private readonly ILogger<CampaignsController> _logger;
	private readonly ICampaignAppService _campaignAppService;
	private readonly IMapper _mapper;
	
	public CampaignsController(ILogger<CampaignsController> logger, ICampaignAppService campaignAppService, IMapper mapper)
	{
		_logger = logger;
		_campaignAppService = campaignAppService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IEnumerable<CampaignDto>> Get(string filter)
	{
		var campaigns = await _campaignAppService.GetCampaignsAsync(filter);
		return campaigns.Select(_mapper.Map<CampaignDto>);
	}
}