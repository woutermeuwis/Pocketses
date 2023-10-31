import PageTitle from "../../components/headings/page-title";
import PageHeader from "../../components/layout/page-header";
import {useModal} from "../../components/contexts/modal-provider";
import NewCampaignForm from "../../components/forms/new-campaign-form";
import {TrashIcon, PlusCircleIcon, PencilIcon, UserPlusIcon, PaperAirplaneIcon} from '@heroicons/react/24/outline'
import LoadingSpinner from "../../components/infrastructure/loading-spinner";
import UpdateCampaignForm from "../../components/forms/update-campaign-form";
import {toast} from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import {useNavigate} from "react-router-dom";
import {Campaign} from "../../api/models/campaign-model";
import {useEffect, useState} from "react";
import {useAuth} from "../../components/contexts/auth-context";
import {apiRoutes} from "../../api/api-routes";


const Campaigns = () => {
    const {showModal, closeModal} = useModal();
    const navigate = useNavigate();
    const {http} = useAuth();
    const [campaigns, setCampaigns] = useState<Campaign[]>([]);
    const [isLoading, setIsLoading] = useState(false);

    const logError = (err: any) => {
        console.log(err);
        setIsLoading(false);
    }

    const fetchData = () => {
        setIsLoading(true);
        http.get(apiRoutes.campaigns)
            .then((res) => {
                setCampaigns(res.data);
                setIsLoading(false);
            })
            .catch(logError);
    }

    const onCreateNew = (name: string) => {
        closeModal();
        setIsLoading(true);
        http.post(apiRoutes.campaigns, {name})
            .then(fetchData)
            .catch(logError);
    }

    const onUpdate = (campaign: Campaign) => {
        closeModal();

        const dto = {
            id: campaign.id,
            name: campaign.name
        };

        setIsLoading(true);
        http.patch(apiRoutes.campaign(campaign.id), dto)
            .then(fetchData)
            .catch(logError);
    }

    const onDelete = (campaign: Campaign) => {
        setIsLoading(true);
        http.delete(apiRoutes.campaign(campaign.id))
            .then(fetchData)
            .catch(logError);
    }

    const copyInviteLink = async (id: string) => {
        const link = window.location.origin + "/campaigns/" + id + "/join";
        await navigator.clipboard.writeText(link);
        toast.info("Invite link copied!");
    }

    useEffect(fetchData, []);

    return (
        <>
            <PageHeader>
                <div className={"flex flex-row justify-between"}>
                    <PageTitle>
                        Campaigns
                    </PageTitle>

                    <button className="bg-blue-400 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                            onClick={() => showModal(<NewCampaignForm submit={onCreateNew}/>)}>
                        <PlusCircleIcon className={"block w-6 h-6"}/>
                    </button>
                </div>
            </PageHeader>

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
                            <th className={"border-b font-medium p-4 pl-8 pt-0 pb-3 text-slate-400 text-left"}>
                                Actions
                            </th>
                        </tr>
                        </thead>
                        <tbody className={"bg-white"}>
                        {campaigns.map((c) => (
                            <tr key={c.id}>
                                <td className={"border-b border-slate-100 p-4 pl-8 text-slate-500"}>
                                    {c.id}
                                </td>
                                <td className={"border-b border-slate-100 p-4 pl-8 text-slate-500"}>
                                    {c.name}
                                </td>
                                <td className={"border-b border-slate-100 p-4 pl-8"}>
                                    <div>

                                        <button
                                            className={"bg-blue-400 hover:bg-blue-600 p-2 m-2 rounded-md text-white"}
                                            title={"Details"}
                                            onClick={() => navigate('/campaigns/' + c.id + '/detail')}>
                                            <PaperAirplaneIcon className={"block h-6 w-6"}/>
                                        </button>

                                        <button
                                            className={"bg-blue-400 hover:bg-blue-600 p-2 m-2 rounded-md text-white"}
                                            title={"Edit"}
                                            onClick={() => showModal(<UpdateCampaignForm submit={onUpdate}
                                                                                         current={c}/>)}>
                                            <PencilIcon className={"block h-6 w-6"}/>
                                        </button>

                                        <button
                                            className={"bg-blue-400 hover:bg-blue-600 p-2 m-2 rounded-md text-white"}
                                            title={"Invite"}
                                            onClick={() => copyInviteLink(c.id)}>
                                            <UserPlusIcon className={"block h-6 w-6"}/>
                                        </button>

                                        <button className={"bg-red-400 hover:bg-red-600 p-2 m-2 rounded-md text-white"}
                                                title={"Delete"}
                                                onClick={() => onDelete(c)}>
                                            <TrashIcon className={"block h-6 w-6"}/>
                                        </button>

                                    </div>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                </div>
            </div>

            {isLoading && <LoadingSpinner/>}
        </>
    )
}

export default Campaigns;
