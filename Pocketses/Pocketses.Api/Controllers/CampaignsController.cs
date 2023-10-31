using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocketses.Api.Dto.Campaign.Request;
using Pocketses.Api.Dto.Campaign.Response;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Models;

namespace Pocketses.Api.Controllers;

/// <summary>
/// Campaigns
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class CampaignsController : BaseController
{
	private readonly ICampaignAppService _campaignAppService;
	private readonly IMapper _mapper;

	/// <inheritdoc/>
	public CampaignsController(IHttpContextAccessor http, IMapper mapper, ICampaignAppService campaignAppService) : base(http)
	{
		_campaignAppService = campaignAppService;
		_mapper = mapper;
	}

	/// <summary>
	/// Get all Campaigns
	/// </summary>
	/// <response code="200">Returns the list of Campaigns</response>
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaignsAsync()
	{
		var campaigns = await _campaignAppService.GetCampaignsAsync();
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
	public async Task<ActionResult<CampaignDetailDto>> GetCampaignAsync(Guid id)
	{
		var campaign = await _campaignAppService.GetCampaignAsync(id);
		if (campaign is null)
			return NotFound();

		var dto = _mapper.Map<CampaignDetailDto>(campaign);
		return Ok(dto);
	}

	/// <summary>
	/// Create a new Campaign
	/// </summary>
	/// <param name="createDto">The campaign to be created</param>
	/// <response code="201">Returns the created campaign</response>
	/// <response code="400">The payload could not be processed</response>
	[HttpPost]
	public async Task<ActionResult<CampaignDetailDto>> CreateAsync(CreateCampaignDto createDto)
	{
		var userId = GetUserId();

		if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(createDto?.Name))
			return BadRequest();

		var campaign = _mapper.Map<Campaign>(createDto);
		campaign.DungeonMasterId = userId;

		var created = await _campaignAppService.CreateAsync(campaign);
		var createdDto = _mapper.Map<CampaignDetailDto>(created);
		return CreatedAtAction(nameof(GetCampaignAsync), new { id = createdDto.Id }, createdDto);
	}

	/// <summary>
	/// Delete a Campaign
	/// </summary>
	/// <param name="id">The Id of the Campaign to be deleted</param>
	/// <response code="200">The campaign was successfully deleted</response>
	/// <response code="400">This is not your campaign</response>
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync(Guid id)
	{
		var userId = GetUserId();
		var campaign = await _campaignAppService.GetCampaignAsync(id);

		if (userId != campaign.DungeonMasterId)
			return BadRequest("You are not the DM for this campaign");

		await _campaignAppService.DeleteAsync(id);
		return Ok();
	}

	/// <summary>
	/// Update a given Campaign
	/// </summary>
	/// <param name="id">The Id of the Campaign to update</param>
	/// <param name="updateDto">The updated state of the Campaign</param>
	/// <response code="200">Returns the updated Campaign</response>
	/// <response code="404">The given Campaign was not found</response>
	/// <response code="400">The given Campaign state did not match the requested resourcev</response>
	[HttpPatch("{id}")]
	public async Task<ActionResult<CampaignDetailDto>> UpdateAsync(Guid id, UpdateCampaignDto updateDto)
	{
		if (updateDto.Id != id)
			return BadRequest();

		var campaign = await _campaignAppService.GetCampaignAsync(id);
		if (campaign is null)
			return NotFound();

		var userId = GetUserId();
		if (campaign.DungeonMasterId != userId)
			return BadRequest("You are not the DM for this campaign");

		campaign.Name = updateDto.Name;

		campaign = await _campaignAppService.UpdateAsync(campaign);
		var updatedDto = _mapper.Map<CampaignDetailDto>(campaign);

		return Ok(updatedDto);
	}

	/// <summary>
	/// Join a given Campaign
	/// </summary>
	/// <param name="id">The id of the campaign to join</param>
	/// <param name="joinDto">Details of the character to join with</param>
	/// <response code="200">Returns the campaign the user joined</response>
	/// <response code="404">The Campaign was not found</response>
	/// <response code="400">The request body was not valid</response>
	[HttpPost("{id}/join")]
	public async Task<ActionResult<CampaignDetailDto>> JoinAsync(Guid id)
	{
		var userId = GetUserId();

		var campaign = await _campaignAppService.GetCampaignAsync(id);
		if (campaign is null)
			return NotFound();

		// if DM show error
		if (campaign.DungeonMasterId == userId)
			return Conflict("You can not join this campaign since you are the DM!");

		// if already joined, show error
		if (campaign.Players.Select(p => p.Id).Contains(userId))
			return Conflict("You already joined this campaign!");

		// join campaign
		var updated = await _campaignAppService.JoinCurrentUser(id);

		var updatedDto = _mapper.Map<CampaignDetailDto>(updated);
		return Ok(updatedDto);
	}
}