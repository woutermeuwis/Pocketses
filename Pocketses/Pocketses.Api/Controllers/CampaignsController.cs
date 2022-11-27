using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Dto.Campaign.Request;
using Pocketses.Core.Dto.Campaign.Response;
using Pocketses.Core.Models;

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
    public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaigns(string? filter)
    {
        var campaigns = await _campaignAppService.GetCampaignsAsync(filter);
        var dtos = campaigns.Select(_mapper.Map<CampaignDto>);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CampaignDetailDto>> GetCampaign(Guid id)
    {
        var campaign = await _campaignAppService.GetCampaignAsync(id);
        var dto = _mapper.Map<CampaignDto>(campaign);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CampaignDto>> Create(CreateCampaignDto createDto)
    {
        var campaign = _mapper.Map<Campaign>(createDto);
        var created = await _campaignAppService.CreateAsync(campaign);
        var createdDto = _mapper.Map<CampaignDto>(created);
        return CreatedAtAction(nameof(GetCampaign), new { id = createdDto.Id }, createdDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
         await _campaignAppService.DeleteAsync(id);
    }
}