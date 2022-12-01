using Pocketses.Core.Models;

namespace Pocketses.Core.AppServices.Interfaces;

public interface ICharacterAppService
{
	Task<Character> CreateAsync(Character campaign);
}
