using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Dto.Campaign.Request;
using Pocketses.Core.Dto.Campaign.Response;
using Pocketses.Core.Models;

namespace Pocketses.Api.Controllers;

/// <summary>
/// Campaigns
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class CampaignsController : ControllerBase
{
	private readonly ILogger<CampaignsController> _logger;
	private readonly ICampaignAppService _campaignAppService;
	private readonly IMapper _mapper;

	/// <inheritdoc/>
	public CampaignsController(ILogger<CampaignsController> logger, ICampaignAppService campaignAppService, IMapper mapper)
	{
		_logger = logger;
		_campaignAppService = campaignAppService;
		_mapper = mapper;
	}

	/// <summary>
	/// Get all Campaigns
	/// </summary>
	/// <param name="filter">Campaign name search query</param>
	/// <response code="200">Returns the list of Campaigns</response>
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaigns(string? filter)
	{
		var campaigns = await _campaignAppService.GetCampaignsAsync(filter);
		var dtos = campaigns.Select(_mapper.Map<CampaignDto>);

		return Ok(dtos);
	}

	/// <summary>
	/// Get Campaign details by Id
	/// </summary>
	/// <param name="id">The Id of the Campaign to get</param>
	/// <response code="200">Returns the requested Campaign</response>
	/// <response code="404">The requested campaign could not be found</response>
	[HttpGet("{id}")]
	public async Task<ActionResult<CampaignDetailDto>> GetCampaign(Guid id)
	{
		var campaign = await _campaignAppService.GetCampaignAsync(id);
		if (campaign is null)
			return NotFound();

		var dto = _mapper.Map<CampaignDto>(campaign);
		return Ok(dto);
	}

	/// <summary>
	/// Create a new Campaign
	/// </summary>
	/// <param name="createDto">The campaign to be created</param>
	/// <response code="201">Returns the created campaign</response>
	[HttpPost]
	public async Task<ActionResult<CampaignDto>> Create(CreateCampaignDto createDto)
	{
		var campaign = _mapper.Map<Campaign>(createDto);
		var created = await _campaignAppService.CreateAsync(campaign);
		var createdDto = _mapper.Map<CampaignDto>(created);
		return CreatedAtAction(nameof(GetCampaign), new { id = createdDto.Id }, createdDto);
	}

	/// <summary>
	/// Delete a Campaign
	/// </summary>
	/// <param name="id">The Id of the Campaign to be deleted</param>
	/// <response code="200">The campaign was successfully deleted</response>	
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await _campaignAppService.DeleteAsync(id);
		return Ok();
	}

	/// <summary>
	/// Update a given Campaign
	/// </summary>
	/// <param name="id">The Id of the Campaign to update</param>
	/// <param name="updateDto">The updated state of the Campaign</param>
	/// <response code="200">Returns the updated Campaign.</response>
	/// <response code="404">The given Campaign was not found.</response>
	/// <response code="400">The given Campaign state did not match the requested resource.</response>
	[HttpPatch("{id}")]
	public async Task<ActionResult<CampaignDto>> Update(Guid id, UpdateCampaignDto updateDto)
	{
		if (updateDto.Id != id)
			return BadRequest();

		var campaign = await _campaignAppService.GetCampaignAsync(id);
		if (campaign is null)
			return NotFound();

		campaign.Name = updateDto.Name;

		var updated = await _campaignAppService.UpdateAsync(campaign);

		var updatedDto = _mapper.Map<CampaignDto>(updated);

		return Ok(updatedDto);
	}
}