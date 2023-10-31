using Pocketses.Core.Models;

namespace Pocketses.Core.Repositories.Interfaces;

public interface ICharacterRepository : IRepository<Character>
{
	Task<List<Character>> GetCharactersForUserAsync(string userId);
}
