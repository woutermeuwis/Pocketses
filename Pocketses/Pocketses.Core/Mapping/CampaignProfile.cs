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
            CreateMap<Campaign, CampaignDto>();

            CreateMap<CreateCampaignDto, Campaign>()
                .IgnoreId()
                .IgnoreAuditedEntity()
                .Ignore(c=>c.Players)
                .Ignore(c=>c.Characters);

            CreateMap<UpdateCampaignDto, Campaign>()
                .IgnoreAuditedEntity()
                .Ignore(c=>c.Players)
                .Ignore(c=>c.Characters);
        }
    }
}
