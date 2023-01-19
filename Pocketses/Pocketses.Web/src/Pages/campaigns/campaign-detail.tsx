import PageHeader from "../../components/layout/page-header";
import PageTitle from "../../components/headings/page-title";
import {useNavigate, useParams} from "react-router-dom";
import {useGetCampaignDetail} from "../../api/utils/campaign-utils";
import LoadingSpinner from "../../components/infrastructure/loading-spinner";

const CampaignDetail = () => {
    const {campaignId} = useParams();
    const {} = useNavigate();

    const {campaign, error: getCampaignError, status: getCampaignStatus} = useGetCampaignDetail(campaignId ?? '');

    const isLoading = getCampaignStatus === 'loading';
    const isLoaded = getCampaignStatus === 'success';
    const isError = getCampaignStatus === 'error';

    const getErrorsView = () => {
        return (
            <>
            </>
        );
    }

    const getCampaignDetailView = () => {
        return (
            <>
                <div className={"bg-slate-50 rounded-xl mx-auto my-4 w-fit "}>
                    <div className={"shadow-sm pt-8 flex justify-center"}>
                        <table className={"table-auto text-sm border-collapse"}>
                            <thead>
                            <tr>
                                <th className={"border-b font-medium p-4 pl-8 pt-0 pb-3 text-slate-400 text-left"}>
                                    Id
                                </th>
                                <th className={"border-b font-medium p-4 pl-8 pt-0 pb-3 text-slate-400 text-left"}>
                                    Name
                                </th>
                            </tr>
                            </thead>
                            <tbody className={"bg-white"}>
                            {campaign.characters.map((c)=>(
                                <tr key={c.id}>
                                    <td className={"border-b border-slate-100 p-4 pl-8 text-slate-500"}>
                                        {c.id}
                                    </td>
                                    <td className={"border-b border-slate-100 p-4 pl-8 text-slate-500"}>
                                        {c.name}
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </>
        );
    }

    return (
        <>
            <PageHeader>
                <div className={"flex flex-row justify-between"}>
                    <PageTitle>
                        {campaign?.name}
                    </PageTitle>
                </div>
            </PageHeader>

            {isError && getErrorsView()}
            {isLoaded && getCampaignDetailView()}
            {isLoading && <LoadingSpinner/>}
        </>
    )
}

export default CampaignDetail;