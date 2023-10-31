using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pocketses.Api.Dto.Characters.Requests;
using Pocketses.Api.Dto.Characters.Response;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.Extensions;
using Pocketses.Core.Models;

namespace Pocketses.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CharactersController : BaseController
{
	private ICharacterAppService _characterAppService;
	private IMapper _mapper;

	public CharactersController(IHttpContextAccessor http, ICharacterAppService characterAppService, IMapper mapper) : base(http)
	{
		_characterAppService = characterAppService;
		_mapper = mapper;
	}

	/// <summary>
	/// Get all Characters
	/// </summary>
	/// <returns code="200">Returns the list of Characters</returns>
	[HttpGet]
	public async Task<ActionResult<CharacterDto>> GetCharactersAsync()
	{
		var characters = await _characterAppService.GetCharactersAsync();
		var dtos = characters.Select(_mapper.Map<CharacterDto>);
		return Ok(dtos);

	}

	/// <summary>
	/// Get Character details by id
	/// </summary>
	/// <param name="id">The Id of the Character to get</param>
	/// <returns code="200">Returns the requested character</returns>
	/// <returns code="404">The requested Character could not be found</returns>
	[HttpGet("{id}")]
	public async Task<ActionResult<CharacterDetailDto>> GetCharacterAsync(Guid id)
	{
		var character = await _characterAppService.GetCharacterAsync(id);
		if (character is null)
			return NotFound();

		var dto = _mapper.Map<CharacterDetailDto>(character);
		return Ok(dto);
	}

	/// <summary>
	/// Create a new Character
	/// </summary>
	/// <param name="createDto">The Character to be created</param>
	/// <returns code="201">Returns the created Character</returns>
	/// <returns code="400">The payload could not be processed</returns>
	[HttpPost]
	public async Task<ActionResult<CharacterDto>> CreateAsync(CreateCharacterDto createDto)
	{
		var userId = GetUserId();

		if (userId.IsNullOrEmpty() || !createDto.CampaignId.IsGuid() || createDto.Name.IsNullOrEmpty())
			return BadRequest();

		var character = _mapper.Map<Character>(createDto);
		character.UserId = userId;

		var created = await _characterAppService.CreateAsync(character);
		var createdDto = _mapper.Map<CharacterDto>(created);
		return CreatedAtAction(nameof(GetCharacterAsync), new { id = createdDto.Id }, createdDto);
	}

	/// <summary>
	/// Delete a Character
	/// </summary>
	/// <param name="id">The id of the Character to be deleted</param>
	/// <returns code="200">The Character was successfully deleted</returns>
	/// <returns code="400">This is not your character</returns>
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync(Guid id)
	{
		var userId = GetUserId();
		var character = await _characterAppService.GetCharacterAsync(id);

		if (userId != character.UserId)
			return BadRequest("You are not the owner of this character");

		await _characterAppService.DeleteAsync(id);
		return Ok();
	}

	/// <summary>
	/// Update a given Character
	/// </summary>
	/// <param name="id">The id of the Character to be updated</param>
	/// <param name="updateDto">The updated state of the Character</param>
	/// <response code="200">Returns the updated Character</response>
	/// <response code="404">The given Character was not found</response>
	/// <response code="400">The given Character state did not match the given Id, or you do not own this Character</response>	
	[HttpPatch("{id}")]
	public async Task<ActionResult<CharacterDetailDto>> UpdateAsync(Guid id, UpdateCharacterDto updateDto)
	{
		return await Task.FromResult<ActionResult<CharacterDetailDto>>(null);
	}
}
