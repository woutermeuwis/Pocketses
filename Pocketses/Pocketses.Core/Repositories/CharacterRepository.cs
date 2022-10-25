using Pocketses.Core.Attributes;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.Repositories;

[ScopedDependency]
public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
{
    public CharacterRepository(PocketsesContext ctx) : base(ctx) { }
}
