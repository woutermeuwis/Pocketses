using Pocketses.Core.Attributes;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.Repositories;

[ScopedDependency]
public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
{
    public CampaignRepository(PocketsesContext ctx) : base(ctx) { }
}
