import {useAuth} from "../../components/contexts/auth-context";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";
import {apiRoutes} from "../api-routes";
import {MutationConfig, QueryConfig} from "../../../vite-env";

export interface Campaign {
    name: string,
    id: string
}

const getCampaignsConfig = (query?: string): QueryConfig => {
    const {http} = useAuth();
    return ({
        queryKey: [apiRoutes.campaigns, query],
        queryFn: () => http.get(`${apiRoutes.campaigns}?filter=${query ?? ''}`).then(res => res.data)
    });
}


const createCampaignConfig = (): MutationConfig<{ name: string }> => {
    const {http} = useAuth();
    const queryClient = useQueryClient();
    return ({
        mutationFn: ({name}) => http.post(apiRoutes.campaigns, {name}).then(res => res.data),
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: [apiRoutes.campaigns]})
        }
    })
}

const deleteCampaignConfig = (): MutationConfig<{ id: string }> => {
    const {http} = useAuth();
    const queryClient = useQueryClient();
    return ({
        mutationFn: ({id}) => http.delete(`${apiRoutes.campaigns}/${id}`),
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: [apiRoutes.campaigns]})
        }
    });
}

const updateCampaignConfig = (): MutationConfig<Campaign> => {
    const {http} = useAuth();
    const queryClient = useQueryClient();
    return {
        mutationFn: ({id, name}) => http.patch(`${apiRoutes.campaigns}/${id}`, {name}).then(res => res.data),
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: [apiRoutes.campaigns]})
        }
    };
}


function useGetCampaigns(query?: string) {
    const result = useQuery(getCampaignsConfig(query));
    return {...result, campaigns: (result.data ?? []) as Campaign[]};
}

function useCreateCampaign() {
    const result = useMutation(createCampaignConfig());
    return {...result, campaign: result.data};
}

function useDeleteCampaign() {
    return useMutation(deleteCampaignConfig());
}

function useUpdateCampaign() {
    const result = useMutation(updateCampaignConfig());
    return {...result, campaign: result.data};
}

export {useGetCampaigns, useCreateCampaign, useDeleteCampaign, useUpdateCampaign}