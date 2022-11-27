import PageTitle from "../../components/headings/page-title"
import PageHeader from "../../components/layout/page-header"
import NewCampaignForm from "../../components/forms/new-campaign-form";

const CreateCampaign = () => {
    return (
        <>
            <PageHeader>
                <PageTitle>
                    Create Campaign
                </PageTitle>
            </PageHeader>

            <div className={"mt-8 mx-4"}>
                <NewCampaignForm/>
            </div>
        </>
    )
};

export default CreateCampaign;