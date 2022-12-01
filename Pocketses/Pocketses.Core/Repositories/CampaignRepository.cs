using Microsoft.EntityFrameworkCore;
using Pocketses.Core.Attributes;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Extensions;
using Pocketses.Core.Models;
using Pocketses.Core.Models.Specifications;
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
			.Include(c => c.Characters)
			.SingleOrDefaultAsync(c => c.Id == id);
	}

	public override Task<List<Campaign>> GetAllAsync()
	{
		return Table
			.Include(c => c.DungeonMaster)
			.ToListAsync();
	}
	public override Task<List<Campaign>> GetAllAsync(ISpecification<Campaign> specification)
	{
		return Table
			.Include(c => c.DungeonMaster)
			.AsQueryable()
			.EvaluateSpecification(specification)
			.ToListAsync();
	}
	public override async Task<Campaign> UpdateAsync(Campaign entity)
	{
		await base.UpdateAsync(entity);
		return await GetAsync(entity.Id);
	}
}