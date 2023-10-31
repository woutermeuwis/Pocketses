using Pocketses.Api.Dto.Characters.Response;

namespace Pocketses.Api.Dto.Campaign.Response
{
	public sealed class CampaignDetailDto : CampaignDto
	{
		/// <summary>
		/// Character property information
		/// </summary>
		public List<CharacterDto> Characters { get; set; }
	}
}
