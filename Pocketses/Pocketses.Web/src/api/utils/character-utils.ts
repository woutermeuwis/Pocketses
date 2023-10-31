import {useMutation, useQueryClient} from "@tanstack/react-query";
import {MutationConfig} from "../../../vite-env";
import {useAuth} from "../../components/contexts/auth-context";
import {apiRoutes} from "../api-routes";

const createCharacterConfig = (): MutationConfig<{ name: string }> => {
    const {http} = useAuth();
    const queryClient = useQueryClient();
    return ({
        mutationFn: ({name}) => http.post(apiRoutes.characters, {name}).then(res => res.data),
        onSuccess: async () => {
            await queryClient.invalidateQueries({queryKey: [apiRoutes.characters]})
        }
    });
}

function useCreateCharacter() {
    const result = useMutation(createCharacterConfig());
    return {...result, character: result.data};
}

export {useCreateCharacter}