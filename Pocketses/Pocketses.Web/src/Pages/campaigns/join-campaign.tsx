import {useNavigate, useParams} from "react-router-dom";
import PageHeader from "../../components/layout/page-header";
import PageTitle from "../../components/headings/page-title";
import {useGetCampaignDetail, useJoinCampaign} from "../../api/utils/campaign-utils";
import LoadingSpinner from "../../components/infrastructure/loading-spinner";

const JoinCampaign = () => {
    const {campaignId} = useParams();
    const navigate = useNavigate();

    const {campaign, error: getError, status: getStatus} = useGetCampaignDetail(campaignId ?? '');
    const {mutate: joinCampaign, status: joinStatus, error: joinError} = useJoinCampaign();

    const isLoading = getStatus === 'loading' || joinStatus === 'loading';
    const isError = getStatus === 'error' || joinStatus === 'error';

    if(joinStatus === 'error' || joinStatus === 'success')
        navigate('/campaigns/'+ campaign.id + '/detail');

    function combineErrors() {
        if (!isError) return [];
        const errors: Error[] = [];
        if (getError) errors.push(getError as Error);
        if (joinError) errors.push(joinError as Error);
        return errors;
    }

    return (
        <>
            <PageHeader>
                <div className={"flex flex-row justify-between"}>
                    <PageTitle>
                        Campaign
                    </PageTitle>
                </div>
            </PageHeader>

            {isError &&
                (<ul className={"mx-auto max-w-4xl m-4"}>
                    {combineErrors().map(e => (
                        <li key={e.message}>
                            <div className={"bg-red-200 border rounded-md p-4"}>
                                <p className={"text-lg font-bold text-red-800 text-center"}>
                                    {e.message}
                                </p>
                            </div>
                        </li>))}
                </ul>)
            }

            {getStatus === 'success' &&
                (<div className={"bg-slate-50 rounded-xl mx-auto my-4 w-fit shadow-sm flex flex-col justify-center"}>
                        <div className={"p-4 px-8 text-lg font-bold text-center"}>
                            {campaign.name}
                        </div>
                        <div className={"bg-white p-4 rounded-b-xl flex flex-col text-center"}>
                            Would you like to join this campaign?
                            <button className={"bg-blue-400 hover:bg-blue-600 mt-6 p-2 m-2 rounded-md text-white"}
                                    onClick={() => joinCampaign({id: campaign.id})}>
                                Join
                            </button>
                        </div>
                    </div>
                )
            }

            {isLoading && <LoadingSpinner/>}
        </>
    )
}

export default JoinCampaign;