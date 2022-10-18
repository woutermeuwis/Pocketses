using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.Repositories;

internal class PlayerRepository : BaseRepository<Player>, IPlayerRepository
{
    public PlayerRepository(PocketsesContext ctx) : base(ctx) { }
}
