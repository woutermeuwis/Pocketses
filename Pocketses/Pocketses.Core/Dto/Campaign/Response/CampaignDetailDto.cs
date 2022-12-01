using Pocketses.Core.Dto.Characters.Response;

namespace Pocketses.Core.Dto.Campaign.Response
{
	public class CampaignDetailDto : CampaignDto
	{
		public List<CharacterDto> Characters { get; set; }
	}
}
