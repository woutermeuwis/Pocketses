using Pocketses.Core.Attributes;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.Repositories;

[ScopedDependency]
public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
{
    public PlayerRepository(PocketsesContext ctx) : base(ctx) { }
}
