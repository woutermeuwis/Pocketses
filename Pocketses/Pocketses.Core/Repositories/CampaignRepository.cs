using Microsoft.EntityFrameworkCore;
using Pocketses.Core.Attributes;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.Repositories;

[ScopedDependency]
public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
{
	public CampaignRepository(PocketsesContext ctx) : base(ctx) { }

	public override async Task<Campaign> CreateAsync(Campaign entity)
	{
		await base.CreateAsync(entity);
		return await GetAsync(entity.Id);
	}

	public override Task<Campaign> GetAsync(Guid id)
	{
		return Table
			.Include(c => c.DungeonMaster)
			.Include(c => c.Players)
			.Include(c => c.Characters)
			.SingleOrDefaultAsync(c => c.Id == id);
	}

	public Task<List<Campaign>> GetCampaignsAsync(Guid[] ids)
	{
		return Table
			.Where(c => ids.Contains(c.Id))
			.ToListAsync();
	}

	public override async Task<Campaign> UpdateAsync(Campaign entity)
	{
		await base.UpdateAsync(entity);
		return await GetAsync(entity.Id);
	}
}