using Pocketses.Core.Models;

namespace Pocketses.Core.AppServices.Interfaces;

public interface ICharacterAppService
{
	Task<List<Character>> GetCharactersAsync();
	Task<Character> GetCharacterAsync(Guid id);
	Task<Character> CreateAsync(Character campaign);
	Task<Character> UpdateAsync(Character campaign);
	Task DeleteAsync(Guid id);
}
