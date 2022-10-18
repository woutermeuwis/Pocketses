namespace Pocketses.Core.Models.Specifications.Campaigns
{
    internal class CampaignNameSpecification : ISpecification<Campaign>
    {
        private string _name;
        private StringComparison _comparisonType;
        public CampaignNameSpecification(string name, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            _name = name;
            _comparisonType = comparisonType;
        }

        public bool IsSatisfiedBy(Campaign campaign)
        {
            return campaign.Name.Contains(_name, _comparisonType);
        }
    }
}
