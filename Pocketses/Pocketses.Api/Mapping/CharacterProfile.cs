using AutoMapper;
using Pocketses.Api.Dto.Characters.Requests;
using Pocketses.Api.Dto.Characters.Response;
using Pocketses.Core.Extensions;
using Pocketses.Core.Models;

namespace Pocketses.Core.Mapping;
public sealed class CharacterProfile : Profile
{
	public CharacterProfile()
	{
		CreateMap<Character, CharacterDto>()
			.MapFrom(c => c.Campaign, c => c.Campaign.Name);

		CreateMap<CreateCharacterDto, Character>()
			.IgnoreAuditedEntity()
			.Ignore(c => c.UserId)
			.Ignore(c => c.User)
			.Ignore(c => c.Campaign)
			.Ignore(c => c.Id);
	}
}
