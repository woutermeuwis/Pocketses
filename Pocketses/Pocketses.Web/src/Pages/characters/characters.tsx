import PageTitle from "../../components/headings/page-title"
import PageHeader from "../../components/layout/page-header"
import {useAuth} from "../../components/contexts/auth-context";
import {useEffect, useState} from "react";
import {Character} from "../../api/models/character-model";
import {apiRoutes} from "../../api/api-routes";
import {PaperAirplaneIcon, TrashIcon} from "@heroicons/react/24/outline";
import LoadingSpinner from "../../components/infrastructure/loading-spinner";
import {useNavigate} from "react-router-dom";

const Characters = () => {
    const {http} = useAuth();
    const navigate = useNavigate();
    const [characters, setCharacters] = useState<Character[]>([])
    const [isLoading, setIsLoading] = useState(false);

    const logError = (err: any) => {
        console.log(err);
        setIsLoading(false);
    }

    const fetchData = () => {
        setIsLoading(true);
        http.get(apiRoutes.characters)
            .then((res) => {
                setCharacters(res.data);
                setIsLoading(false);
            })
            .catch(logError);
    }

    const deleteCharacter = (c: Character) => {
        setIsLoading(true);
        http.delete(apiRoutes.character(c.id))
            .then(fetchData)
            .catch(logError);
    }

    useEffect(fetchData, [])


    return (
        <>
            <PageHeader>
                <div className={"flex flex-row justify-between"}>
                    <PageTitle>
                        Characters
                    </PageTitle>
                </div>
            </PageHeader>

            <div className={"bg-slate-50 rounded-xl mx-auto my-4 w-fit"}>
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
                                Campaign
                            </th>

                            <th className={"border-b font-medium p-4 pl-8 pt-0 pb-3 text-slate-400 text-left"}>
                                Actions
                            </th>

                        </tr>
                        </thead>
                        <tbody className={"bg-white"}>
                        {characters.map((c) => (
                            <tr key={c.id}>
                                <td className={"border-b border-slate-100 p-4 pl-8 text-slate-500"}>
                                    {c.id}
                                </td>

                                <td className={"border-b border-slate-100 p-4 pl-8 text-slate-500"}>
                                    {c.name}
                                </td>

                                <td className={"border-b border-slate-100 p-4 pl-8 text-slate-500"}>
                                    {c.campaign}
                                </td>

                                <td className={"border-b border-slate-100 p-4 pl-8"}>
                                    <div>

                                        <button
                                            className={"bg-blue-400 hover:bg-blue-600 p-2 m-2 rounded-md text-white"}
                                            title={"Details"}
                                            onClick={() => navigate('/characters/' + c.id + '/detail')}>
                                            <PaperAirplaneIcon className={"block h-6 w-6"}/>
                                        </button>

                                        <button className={"bg-red-400 hover:bg-red-600 p-2 m-2 rounded-md text-white"}
                                                title={"Delete"}
                                                onClick={() => deleteCharacter(c)}>
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

export default Characters
