import PageTitle from "../../components/headings/page-title";
import PageHeader from "../../components/layout/page-header";
import {useModal} from "../../components/contexts/modal-provider";
import NewCampaignForm from "../../components/forms/new-campaign-form";
import {
    Campaign,
    useCreateCampaign,
    useDeleteCampaign,
    useGetCampaigns,
    useUpdateCampaign
} from "../../api/utils/campaign-utils";
import {TrashIcon, PlusCircleIcon, PencilIcon} from '@heroicons/react/24/outline'
import LoadingSpinner from "../../components/infrastructure/loading-spinner";
import UpdateCampaignForm from "../../components/forms/update-campaign-form";

const Campaigns = () => {
    const {showModal, closeModal} = useModal();

    const {campaigns, error: getError, status: getStatus} = useGetCampaigns();
    const {mutate: deleteCampaign, status: deleteStatus, error: deleteError} = useDeleteCampaign();
    const {mutate: createCampaign, status: creationStatus, error: creationError} = useCreateCampaign();
    const {mutate: updateCampaign, status: updateStatus, error: updateError} = useUpdateCampaign();

    const statusList = [getStatus, deleteStatus, creationStatus, updateStatus];
    const isLoading = statusList.includes('loading');
    const isError = statusList.includes('error');

    function combineErrors() {
        if (!isError) return [];
        const errors: Error[] = [];
        if (getError) errors.push(getError as Error);
        if (deleteError) errors.push(deleteError as Error);
        if (creationError) errors.push(creationError as Error);
        if (updateError) errors.push(updateError as Error);
        return errors;
    }

    const onCreateNew = (name: string) => {
        closeModal();
        createCampaign({name});
    }

    const onUpdate = (campaign: Campaign) =>{
        closeModal();
        updateCampaign(campaign);
    }

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
            {isError &&
                (<ul className={"mx-auto max-w-4xl"}>
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

            (
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
                                        <button className={"bg-red-400 hover:bg-red-600 p-2 m-2 rounded-md text-white"}
                                                onClick={() => deleteCampaign({id: c.id})}>
                                            <TrashIcon className={"block h-6 w-6"}/>
                                        </button>
                                        <button className={"bg-blue-400 hover:bg-blue-600 p-2 m-2 rounded-md text-white"}
                                                onClick={() => showModal(<UpdateCampaignForm submit={onUpdate} current={c}/>)}>
                                            <PencilIcon className={"block h-6 w-6"}/>
                                        </button>

                                    </div>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                </div>
            </div>
            )

            {isLoading && <LoadingSpinner/>}
        </>
    )
}

export default Campaigns;