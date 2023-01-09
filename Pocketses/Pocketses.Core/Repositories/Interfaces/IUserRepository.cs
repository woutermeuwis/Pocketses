using Pocketses.Core.Models;

namespace Pocketses.Core.Repositories.Interfaces;
public interface IUserRepository : IRepository<User>
{
	Task<User> GetAsync(string id);
	Task<User> GetWithCampaignsAsync(string id);
}
