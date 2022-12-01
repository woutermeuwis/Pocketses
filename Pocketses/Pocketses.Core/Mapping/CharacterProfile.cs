using AutoMapper;
using Pocketses.Core.Dto.Characters.Response;
using Pocketses.Core.Models;

namespace Pocketses.Core.Mapping;
public sealed class CharacterProfile : Profile
{
	public CharacterProfile()
	{
		CreateMap<Character, CharacterDto>();
	}
}
