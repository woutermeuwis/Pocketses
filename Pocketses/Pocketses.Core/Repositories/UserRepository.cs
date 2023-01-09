using Microsoft.EntityFrameworkCore;
using Pocketses.Core.Attributes;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.Repositories;

[ScopedDependency]
public class UserRepository : BaseRepository<User>, IUserRepository
{
	public UserRepository(PocketsesContext ctx) : base(ctx)
	{ }

	public override Task<User> GetAsync(Guid id)
	{
		throw new NotImplementedException("User Ids are defined as strings because User inherits from IdentityUser. Please use the overload that accepts the ID as a string.");
	}

	public Task<User> GetAsync(string id)
	{
		return Table.FindAsync(id).AsTask();
	}

	public Task<User> GetWithCampaignsAsync(string id)
	{
		return Table
			.Include(u => u.DmCampaigns)
			.Include(u => u.UserCampaigns)
			.FirstOrDefaultAsync(u => u.Id == id);
	}
}
