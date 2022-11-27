import {useMutation, useQuery} from "@tanstack/react-query";
import {AxiosInstance, AxiosRequestConfig} from "axios";
import {apiRoutes} from "./api-routes";

class apiClient {
    http: AxiosInstance;

    constructor(http: AxiosInstance) {
        this.http = http;
    }
}

export default apiClient;