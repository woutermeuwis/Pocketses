import {useAuth} from "../../components/contexts/auth-context";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";
import {apiRoutes} from "../api-routes";
import {MutationConfig, QueryConfig} from "../../../vite-env";





const joinCampaignConfig = (): MutationConfig<{ id: string }> => {
    const {http} = useAuth();
    const queryClient = useQueryClient();

    return ({
        mutationFn: ({id}) => http.post(`${apiRoutes.campaigns}/${id ?? ''}/join`, {characterName: 'example'}).then(res => res.data),
        onSuccess: async () => {
            await queryClient.invalidateQueries({queryKey: [apiRoutes.campaigns]})
        }
    })
}




function useJoinCampaign() {
    const result = useMutation(joinCampaignConfig());
    return {...result, campaign: result.data};
}

export {
    useJoinCampaign,
}
