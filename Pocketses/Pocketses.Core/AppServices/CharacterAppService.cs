using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Attributes;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.AppServices;

[ScopedDependency]
public class CharacterAppService : ICharacterAppService
{
	private readonly ICharacterRepository _characterRepository;

	public CharacterAppService(ICharacterRepository characterRepository)
	{
		_characterRepository = characterRepository;
	}

	public Task<Character> CreateAsync(Character campaign)
	{
		return _characterRepository.CreateAsync(campaign);
	}
}
