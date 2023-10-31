using Microsoft.AspNetCore.Http;
using Pocketses.Core.AppServices.Base;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Attributes;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.AppServices;

[ScopedDependency]
public class CharacterAppService : BaseAppService, ICharacterAppService
{
	private readonly ICharacterRepository _characterRepository;

	public CharacterAppService(IHttpContextAccessor http, ICharacterRepository characterRepository) : base(http)
	{
		_characterRepository = characterRepository;
	}

	public async Task<Character> GetCharacterAsync(Guid id)
	{
		return await _characterRepository.GetAsync(id);
	}
	public Task<List<Character>> GetCharactersAsync()
	{
		return _characterRepository.GetCharactersForUserAsync(GetUserId());
	}

	public Task<Character> CreateAsync(Character campaign)
	{
		return _characterRepository.CreateAsync(campaign);
	}


	public Task<Character> UpdateAsync(Character campaign)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(Guid id)
	{
		return _characterRepository.DeleteAsync(id);
	}

}
