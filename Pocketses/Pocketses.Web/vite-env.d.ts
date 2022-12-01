import {ReactNode} from "react";
import {MutationFunction, QueryFunction, QueryKey} from "@tanstack/react-query";

type OnlyChildrenProps = { children: ReactNode }

type QueryConfig = {
    queryKey: QueryKey,
    queryFn: QueryFunction,
    config?: {}
}

type MutationConfig<T> = {
    mutationFn: MutationFunction<{}, T>,
    onSuccess: () => void
    config?: {}
}