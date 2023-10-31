using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocketses.Api.Dto.Campaign.Request
{
	public sealed class UpdateCampaignDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
