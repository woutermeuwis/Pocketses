import PageHeader from "../../components/layout/page-header";
import PageTitle from "../../components/headings/page-title";
import {useNavigate, useParams} from "react-router-dom";
import LoadingSpinner from "../../components/infrastructure/loading-spinner";
import {useUser} from "../../components/contexts/user-context";
import NewCharacterForm from "../../components/forms/new-character-form";
import {useEffect, useState} from "react";
import {useAuth} from "../../components/contexts/auth-context";
import {apiRoutes} from "../../api/api-routes";
import { CampaignInfo} from "../../api/models/campaign-model";

const CampaignDetail = () => {
    const {http} = useAuth();
    const {campaignId} = useParams();
    const {} = useNavigate();
    const user = useUser();
    const [campaign, setCampaign] = useState<CampaignInfo>();
    const [isLoading, setIsLoading] = useState(false);

    const logError = (err: any) => {
        console.log(err);
        setIsLoading(false);
    }

    const fetchData = () => {
        setIsLoading(true);
        http.get(apiRoutes.campaign(campaignId ?? ''))
            .then(res => {
                setCampaign(res.data);
                setIsLoading(false);
            })
            .catch(logError)
    }

    const onCreateNewCharacter = (name: string) => {
        setIsLoading(true);
        const dto = {name: name, campaignId: campaignId};
        http.post(apiRoutes.characters, dto)
            .then(fetchData)
            .catch(logError);
    }

    const charactersTable = () => {
        return (
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
                {campaign?.characters.map((c) => (
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
            </table>);
    }

    const noCharacterMessage = () => {
        return (<NewCharacterForm submit={onCreateNewCharacter}/>);
    }

    const getCampaignDetailView = () => {
        const hasCharacter = campaign?.characters.map(c => c.userId).includes(user.userId);
        console.log('hasCharacter: ' + hasCharacter);
        console.log('campaign:');
        console.log(campaign);
        console.log('userId = ' +user.userId);
        console.log(campaign?.characters.map(c => c.userId));
        return (
            <>
                <div className={"bg-slate-50 rounded-xl mx-auto my-4 w-fit "}>
                    <div className={"shadow-sm pt-8 flex justify-center"}>
                        {hasCharacter ? charactersTable() : noCharacterMessage()}
                    </div>
                </div>
            </>
        );
    }


    useEffect(fetchData, []);
    return (
        <>
            <PageHeader>
                <div className={"flex flex-row justify-between"}>
                    <PageTitle>
                        {campaign?.name}
                    </PageTitle>
                </div>
            </PageHeader>

            {!isLoading && getCampaignDetailView()}
            {isLoading && <LoadingSpinner/>}
        </>
    )
}

export default CampaignDetail;
