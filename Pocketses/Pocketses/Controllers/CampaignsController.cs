using Pocketses.Core.AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Pocketses.Web.Models.Campaigns;

namespace Pocketses.Web.Controllers;
public class CampaignsController : Controller
{
    private readonly ICampaignAppService _campaignService;

    public CampaignsController(ICampaignAppService campaignService)
    {
        _campaignService = campaignService;
    }

    // GET: CampaignsController
    public async Task<ActionResult> Index(string searchString)
    {
        var campaigns = await _campaignService.GetCampaignsAsync(searchString);
        var vm = new CampaignsViewModel
        {
            Campaigns = campaigns
                .Select(c => new Campaign
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList()
        };
        return View(vm);
    }

    // GET: CampaignsController/Details/5
    public async Task<ActionResult> Details(Guid? id)
    {
        if (id is null)
            return NotFound();

        var campaign = await _campaignService.GetCampaignAsync(id.Value);
        if (campaign is null)
            return NotFound();

        var vm = new Campaign { Id = campaign.Id, Name = campaign.Name };
        return View(vm);
    }

    // GET: CampaignsController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: CampaignsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Campaign campaign)
    {
        if (ModelState.IsValid)
        {
            var toCreate = new Core.Models.Campaign { Name = campaign.Name };
            await _campaignService.CreateAsync(toCreate);
            return RedirectToAction(nameof(Index));
        }

        return View(campaign);
    }

    // GET: CampaignsController/Edit/5
    public async Task<ActionResult> Edit(Guid? id)
    {
        if (id is null)
            return NotFound();

        var campaign = await _campaignService.GetCampaignAsync(id.Value);

        if (campaign is null)
            return NotFound();

        var vm = new Campaign
        {
            Id = campaign.Id,
            Name = campaign.Name
        };
        return View(vm);
    }

    // POST: CampaignsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Guid? id, Campaign campaign)
    {
        if (id != campaign.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var toUpdate = new Core.Models.Campaign { Id = campaign.Id, Name = campaign.Name };
            await _campaignService.UpdateAsync(toUpdate);
            return RedirectToAction(nameof(Index));
        }

        return View(campaign);
    }

    // GET: CampaignsController/Delete/5
    public async Task<ActionResult> Delete(Guid? id)
    {
        if (id is null)
            return NotFound();

        var campaign = await _campaignService.GetCampaignAsync(id.Value);
        if (campaign is null)
            return NotFound();

        var vm = new Campaign
        {
            Id = campaign.Id,
            Name = campaign.Name
        };
        return View(vm);
    }

    // POST: CampaignsController/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid? id)
    {
        if (id is null)
            return NotFound();

        await _campaignService.DeleteAsync(id.Value);
        return RedirectToAction(nameof(Index));
    }
}
