namespace Pocketses.Api.Dto.Characters.Response;
public class CharacterDto
{
	public Guid Id { get; set; }
	public Guid CampaignId { get; set; }
	public string UserId { get; set; }
	public string Name { get; set; }
	public string Campaign { get; set; }
}

