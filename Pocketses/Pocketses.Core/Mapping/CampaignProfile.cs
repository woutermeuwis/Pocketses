using AutoMapper;
using Pocketses.Core.Dto.Campaign.Request;
using Pocketses.Core.Dto.Campaign.Response;
using Pocketses.Core.Extensions;
using Pocketses.Core.Models;

namespace Pocketses.Core.Mapping
{
    public class CampaignProfile : Profile
    {
        public CampaignProfile()
        {
            CreateMap<Campaign, CampaignDto>()
	            .MapFrom(c=>c.DmId, c=>c.DungeonMasterId)
	            .MapFrom(c=>c.Dm, c=>c.DungeonMaster.UserName);

            CreateMap<Campaign, CampaignDetailDto>()
	            .MapFrom(c=>c.DmId, c=>c.DungeonMasterId)
	            .MapFrom(c=>c.Dm, c=>c.DungeonMaster.UserName);

            CreateMap<CreateCampaignDto, Campaign>()
                .IgnoreId()
                .IgnoreAuditedEntity()
                .Ignore(c=>c.Characters)
                .Ignore(c=>c.DungeonMaster)
                .Ignore(c=>c.DungeonMasterId);

            CreateMap<UpdateCampaignDto, Campaign>()
                .IgnoreAuditedEntity()
                .Ignore(c=>c.Characters)
                .Ignore(c=>c.DungeonMaster)
                .Ignore(c=>c.DungeonMasterId);
        }
    }
}
